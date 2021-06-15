using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService
{
    public interface IBenefitsDataSource
    {
        public List<Employee> GetBeneficiariesGivenEomplyerID(int employerID);
        public double GetCostGivenBeneficiary(Beneficiary beneficiary);

        public List<NameStartsWithDiscountRate> GetNameStartsWithDiscountRates();

        public void AddEmployee(BeneficiaryRequestData employee);

        public bool IsEmployerAvailable(int employerID);
        void AddEmployer(int emploerID);
    }
}
