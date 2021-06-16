using System;
using System.Collections.Generic;
using System.Text;
using BenefitsDataService.BenefitsData;

namespace BenefitsService.BenefitsService.BenefitsCostCalulator
{
    public interface IBenefitsCostCalculator
    {
        public double CalculateBenefitsCostGivenBeneficiary(Beneficiary beneficiary);
    }
}
