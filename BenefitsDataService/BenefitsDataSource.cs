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
                .Include(s=> s.BeneficiaryType).Include(s=> s.Dependents).ThenInclude(s=>s.BeneficiaryType).ToList();
        }

        public double GetAnnualBenefitsCostGivenBeneficiary(Beneficiary beneficiary)
        {
            return decimal.ToDouble(_benefitsDbContext.Costs.Where(s => s.BeneficiaryTypeFK == beneficiary.BeneficiaryTypeFK
            && s.EmployerFK == beneficiary.EmployerFK).Select(s => s.Amount).FirstOrDefault());
        }

        public List<NameStartsWithDiscountRate> GetNameStartsWithDiscountRates()
        {
            return _benefitsDbContext.NameStartsWithDiscountRates.ToList();
        }

        public void AddNewEmployee(Employee employee)
        {
            employee.BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Employee);
            _benefitsDbContext.Employees.Add(employee);

            _benefitsDbContext.SaveChanges();
            _benefitsDbContext.Employees.Attach(employee);
        }

        public void AddDependent(Dependent dependent)
        {
            dependent.BeneficiaryTypeFK = GetBeneficiaryTypeID(Enums.BeneficiaryType.Dependent);
            _benefitsDbContext.Dependents.Add(dependent);
            _benefitsDbContext.SaveChanges();
        }

        public int GetBeneficiaryTypeID(Enums.BeneficiaryType beneficiaryType)
        {
            return _benefitsDbContext.BeneficiaryTypes.Where(s => s.PersonType == beneficiaryType.ToString())
                .FirstOrDefault().ID;
        }

        public bool IsEmployerAvailable(int employerID)
        {
            return _benefitsDbContext.Employers.Find(employerID) != null;
        }

        public void AddNewEmployer(int employerID)
        {
            var employer = new Employer
            {
                ID = employerID,
                Name = Constant.EmployerName
            };
            _benefitsDbContext.Employers.Add(employer);
            _benefitsDbContext.SaveChanges();
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
            _benefitsDbContext.SaveChanges();
            _benefitsDbContext.Costs.Add(employeeCost);
            _benefitsDbContext.SaveChanges();
        } 
    }
}