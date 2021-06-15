using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BenefitsDataService;
using BenefitsDataService.BenefitsData;

namespace BenefitsService.BenefitsService.BenefitsDiscountCalculator
{
    public class DiscountCalculatorByNamePrefix : IDiscountCalculator
    {
        private readonly IBenefitsDataSource _benefitsDataSource;

        public DiscountCalculatorByNamePrefix(IBenefitsDataSource benefitsDataSource)
        {
            _benefitsDataSource = benefitsDataSource;
        }

        public double CalculateDiscountGivenBeneficiaryAndCost(Beneficiary beneficiary, double cost)
        {
            var nameStartwithRates = _benefitsDataSource.GetNameStartsWithDiscountRates();
            var discountRate = double.MinValue;
            foreach (var nameStartwithRate in nameStartwithRates)
            {
                if(beneficiary.FirstName.StartsWith(nameStartwithRate.PrefixString))
                {
                    discountRate = Math.Max(discountRate, nameStartwithRate.Rate);
                }
            }

            return discountRate == double.MinValue ? 0 : cost * discountRate / 100;
        }
    }
}
