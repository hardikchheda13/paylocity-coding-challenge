using BenefitsDataService;
using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsService.BenefitsService.BenefitsDiscountCalculator
{
    public class BenefitsDiscountCalculator : IDiscountCalculator
    {
        private readonly IBenefitsDataSource _benefitsDataSource;

        public BenefitsDiscountCalculator(IBenefitsDataSource benefitsDataSource)
        {
            _benefitsDataSource = benefitsDataSource;
        }

        public double CalculateDiscountGivenBeneficiaryAndCost(Beneficiary beneficiary, double cost)
        {
            CompositeDiscountCalculator discountCalculator = new CompositeDiscountCalculator();
            discountCalculator.Add(new DiscountCalculatorByNamePrefix(_benefitsDataSource));
            return discountCalculator.CalculateDiscountGivenBeneficiaryAndCost(beneficiary, cost);
        }
    }
}
