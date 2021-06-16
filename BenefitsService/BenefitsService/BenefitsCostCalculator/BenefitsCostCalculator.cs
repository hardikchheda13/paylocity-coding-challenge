using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BenefitsService.BenefitsService.BenefitsDiscountCalculator;
using BenefitsDataService.BenefitsData;
using BenefitsDataService;

namespace BenefitsService.BenefitsService.BenefitsCostCalulator
{
    public class BenefitsCostCalculator: IBenefitsCostCalculator
    {
        private readonly IBenefitsDataSource _benefitsDataSource;
        private readonly IDiscountCalculator _discountCalculator;

        public BenefitsCostCalculator(IBenefitsDataSource benefitsDataSource, IDiscountCalculator discountCalculator)
        {
            _benefitsDataSource = benefitsDataSource;
            _discountCalculator = discountCalculator;
        }

        public double CalculateBenefitsCostGivenBeneficiary(Beneficiary beneficiary)
        {
            var benefitsCost = _benefitsDataSource.GetAnnualBenefitsCostGivenBeneficiary(beneficiary); 
                
                var discount = _discountCalculator.CalculateDiscountGivenBeneficiaryAndCost(beneficiary, benefitsCost);
            return benefitsCost - discount;
        }
    }
}
