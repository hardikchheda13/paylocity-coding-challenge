using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsService.BenefitsService
{
    public interface IBenefitsService
    {
        public string CalculateBenefitsCostForGivenEmployer(int employerID);

        public string AddNewEmployee(BeneficiaryRequestData employee);
    }
}
