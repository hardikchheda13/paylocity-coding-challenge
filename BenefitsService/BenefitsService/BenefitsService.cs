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
                return AddEmployerAndReturnEmptyData(employerID);
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
                    BeneficiaryType = employee.BeneficiaryType.Person_Type,
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

        private string AddEmployerAndReturnEmptyData(int employerID)
        {
            _benefitsDataSource.AddEmployer(employerID);
            return "";
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
                    BeneficiaryType = dependent.BeneficiaryType.Person_Type,
                    Cost = _benefitsCostCalulator.CalculateBenefitsCostGivenBeneficiary(dependent)
                };
                costOfAllDependents += dependentCostDatum.Cost;
            }
            return dependentCostData;
        }

        public string AddEmployee(Employee employee)
        {
            _benefitsDataSource.AddEmployee(employee);
            return CalculateBenefitsCostForGivenEmployer(employee.EmployerFK);
        }
    }
}
