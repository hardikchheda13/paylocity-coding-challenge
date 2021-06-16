using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService.BenefitsData
{
    public interface IBeneficiaryDataSource
    {
        public List<Employee> GetBeneficiariesGivenEomplyerID(int employerID);
        public void AddNewEmployee(Employee employee);
        public void AddDependent(Dependent dependent);
        public int GetBeneficiaryTypeID(Enums.BeneficiaryType beneficiaryType);
    }
}
