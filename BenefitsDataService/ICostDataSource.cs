using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService
{
    public interface ICostDataSource
    {
        public double GetAnnualBenefitsCostGivenBeneficiary(Beneficiary beneficiary);
    }
}
