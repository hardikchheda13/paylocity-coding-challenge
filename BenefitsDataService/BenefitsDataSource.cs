using BenefitsDataService.BenefitsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenefitsDataService
{
    public class BenefitsDataSource : IBenefitsDataSource 
    {
        private readonly BenefitsDbContext _benefitsDbContext;

        public BenefitsDataSource(BenefitsDbContext benefitsDbContext)
        {
            _benefitsDbContext = benefitsDbContext;
        }

        public List<Employee> GetBeneficiariesGivenEomplyerID(int employerID)
        {
            return _benefitsDbContext.Employees.Where(s => s.EmployerFK == employerID)
                .Include(s => s.Dependents).ToList();
        }

        public double GetCostGivenBeneficiary(Beneficiary beneficiary)
        {
            return _benefitsDbContext.Costs.Where(s => s.BeneficiaryTypeFK == beneficiary.BeneficiaryTypeFK
            && s.EmployerFK == beneficiary.EmployerFK).Select(s => s.Amount).FirstOrDefault();
        }

        public List<NameStartsWithDiscountRate> GetNameStartsWithDiscountRates()
        {
            return _benefitsDbContext.NameStartsWithDiscountRates.ToList();
        }

        public void AddEmployee(Employee employee)
        {            
            employee.BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Employee);
            _benefitsDbContext.Employees.Add(employee);

            _benefitsDbContext.SaveChanges();
            foreach(var dependent in employee.Dependents)
            {
                AddDependent(dependent, employee.ID);
            }
        }

        public void AddDependent(Dependent dependent, int employeeID)
        {
            dependent.EmployeeFK = employeeID;
            dependent.BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Dependent);
            _benefitsDbContext.Dependents.Add(dependent);
            _benefitsDbContext.SaveChanges();
        }

        private int GetBeneficiaryTypeID(Enums.BeneficiaryType beneficiaryType)
        {
            return _benefitsDbContext.BeneficiaryTypes.Where(s => s.Person_Type == beneficiaryType.ToString())
                .FirstOrDefault().ID;
        }

        public bool IsEmployerAvailable(int employerID)
        {
            return _benefitsDbContext.Employers.Find(employerID) != null;
        }

        public void AddEmployer(int employerID)
        {
            var employer = new Employer
            {
                ID = employerID,
                Name = Constant.EmployerName
            };
            _benefitsDbContext.Employers.Add(employer);
            var dependentCost = new Cost
            {
                EmployerFK = employerID,
                BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Dependent),
                Amount = Constant.DependentAnnualBenefitsCost
            };
            var employeeCost = new Cost
            {
                EmployerFK = employerID,
                BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Employee),
                Amount = Constant.EmployeeAnnualBenefitsCost
            };
            _benefitsDbContext.Costs.Add(dependentCost);
            _benefitsDbContext.Costs.Add(employeeCost);
            _benefitsDbContext.SaveChanges();
        } 
    }
}