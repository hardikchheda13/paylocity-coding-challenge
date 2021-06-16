using BenefitsDataService;
using System.Runtime.Serialization;
using System.Collections.Generic;
using BenefitsDataService.BenefitsData;
using BenefitsService.BenefitsService.BenefitsCostCalulator;
using BenefitsService.BenefitsService.Utils;

namespace BenefitsService.BenefitsService
{
    public class BenefitsService : IBenefitsService
    {
        private readonly IBenefitsDataSource _benefitsDataSource;
        private readonly IBenefitsCostCalculator _benefitsCostCalulator;
        public BenefitsService(IBenefitsCostCalculator benefitsCostCalulator, IBenefitsDataSource benefitsDataSource)
        {
            _benefitsDataSource = benefitsDataSource;
            _benefitsCostCalulator = benefitsCostCalulator;
        }

        public string CalculateBenefitsCostForGivenEmployer(int employerID)
        {
            if(!_benefitsDataSource.IsEmployerAvailable(employerID))
            {
                AddNewEmployer(employerID);
                return "";
            }

            var employeeList = _benefitsDataSource.GetBeneficiariesGivenEomplyerID(employerID);
            var totalCostForEmployer = 0.0;
            var employeeCostDataList = new List<EmployeeCostData>();
            foreach (var employee in employeeList)
            {
                var employeeCostDatum = new EmployeeCostData
                {
                    ID = employee.ID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    BeneficiaryType = employee.BeneficiaryType.PersonType,
                    Cost = _benefitsCostCalulator.CalculateBenefitsCostGivenBeneficiary(employee),
                    DependentCostData = GetDependentCostData(employee.Dependents, out double costOfAllDependents)
                };
                employeeCostDataList.Add(employeeCostDatum);
                totalCostForEmployer = totalCostForEmployer + employeeCostDatum.Cost + costOfAllDependents;
            }

            var employerCostData = new EmployerCostData
            {
                EmployeeCostDatas = employeeCostDataList,
                TotalCost = totalCostForEmployer
            };
            var json = JsonUtils.ToJson(employerCostData);
            return json;
        }

        private void AddNewEmployer(int employerID)
        {
            _benefitsDataSource.AddNewEmployer(employerID);
        }

        List<DependentCostData> GetDependentCostData(ICollection<Dependent> dependents, out double costOfAllDependents)
        {
            costOfAllDependents = 0;
            List<DependentCostData> dependentCostData = new List<DependentCostData>();
            foreach (var dependent in dependents)
            {
                var dependentCostDatum = new DependentCostData
                {
                    ID = dependent.ID,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    BeneficiaryType = dependent.BeneficiaryType.PersonType,
                    Cost = _benefitsCostCalulator.CalculateBenefitsCostGivenBeneficiary(dependent)
                };
                dependentCostData.Add(dependentCostDatum);
                costOfAllDependents += dependentCostDatum.Cost;
            }
            return dependentCostData;
        }

        public string AddNewEmployee(BeneficiaryRequestData beneficiaryRequestData)
        {
            if (!_benefitsDataSource.IsEmployerAvailable(beneficiaryRequestData.EmployerID))
            {
                AddNewEmployer(beneficiaryRequestData.EmployerID);
            }
            var employee = new Employee
            {
                FirstName = beneficiaryRequestData.FirstName,
                LastName = beneficiaryRequestData.LastName,
                EmployerFK = beneficiaryRequestData.EmployerID
            };
            _benefitsDataSource.AddNewEmployee(employee);
            foreach(var dependent in beneficiaryRequestData.Dependents)
            {
                var newDependent = new Dependent
                {
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    EmployeeFK = employee.ID,
                    EmployerFK = beneficiaryRequestData.EmployerID
                };

                _benefitsDataSource.AddDependent(newDependent);
            }
            return CalculateBenefitsCostForGivenEmployer(employee.EmployerFK);
        }
    }
}
