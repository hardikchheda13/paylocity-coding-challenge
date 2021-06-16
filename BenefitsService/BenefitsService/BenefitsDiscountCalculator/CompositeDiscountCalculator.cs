using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsService.BenefitsService.BenefitsDiscountCalculator
{
    class CompositeDiscountCalculator : IDiscountCalculator
    {
        private readonly ICollection<IDiscountCalculator> discountCalculators; 

        public CompositeDiscountCalculator()
        {
            discountCalculators = new List<IDiscountCalculator>(); 
        }

        public void Add(IDiscountCalculator discountCalculator)
        {
            discountCalculators.Add(discountCalculator);
        }

        public double CalculateDiscountGivenBeneficiaryAndCost(Beneficiary beneficiary, double cost)
        {
            var totalDiscount = 0.0;
            foreach(var discountCalculator in discountCalculators)
            {
                totalDiscount += discountCalculator.CalculateDiscountGivenBeneficiaryAndCost(beneficiary, cost);
            }
            return totalDiscount;
        }
    }
}
