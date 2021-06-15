using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsService.BenefitsService.BenefitsDiscountCalculator
{
    public interface IDiscountCalculator
    {
        public double CalculateDiscountGivenBeneficiaryAndCost(Beneficiary beneficiary, double cost);
    }
}
