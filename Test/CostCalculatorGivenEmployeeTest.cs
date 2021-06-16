using BenefitsDataService;
using BenefitsDataService.BenefitsData;
using BenefitsService.BenefitsService.BenefitsCostCalulator;
using BenefitsService.BenefitsService.BenefitsDiscountCalculator;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class CostCalculatorGivenEmployeeTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CalculateBenefitsCostGivenBeneficiary()
        {
            var dataSourceMoq = new Mock<IBenefitsDataSource>();
            var discountCalculatorMoq = new Mock<IDiscountCalculator>();
            var beneficiary = new Mock<Beneficiary>();
            dataSourceMoq.Setup(s => s.GetAnnualBenefitsCostGivenBeneficiary(beneficiary.Object)).Returns(1000);
            discountCalculatorMoq.Setup(s => s.CalculateDiscountGivenBeneficiaryAndCost(beneficiary.Object, 1000)).Returns(100.00);
            IBenefitsCostCalculator benefitsCostCalculator = new BenefitsCostCalculator(dataSourceMoq.Object, discountCalculatorMoq.Object);
            var cost = benefitsCostCalculator.CalculateBenefitsCostGivenBeneficiary(beneficiary.Object);
            Assert.AreEqual(900, cost);
        }
    }
}