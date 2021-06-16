using BenefitsDataService;
using BenefitsDataService.BenefitsData;
using BenefitsService.BenefitsService.BenefitsDiscountCalculator;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    public class DiscountCalculatorTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CalculateDiscount()
        {
            Beneficiary ben = new Beneficiary
            {
                FirstName = "A",
                LastName = "B",
                EmployerFK = 1,
                BeneficiaryTypeFK = 1
            };
            var dataSourceMoq = new Mock<IBenefitsDataSource>();
            IDiscountCalculator discountCalculatorByNamePrefix = new DiscountCalculatorByNamePrefix(dataSourceMoq.Object);
            dataSourceMoq.Setup(s => s.GetAnnualBenefitsCostGivenBeneficiary(ben)).Returns(1000);
            NameStartsWithDiscountRate nameStartsWithDiscountRate = new NameStartsWithDiscountRate
            {
                PrefixString = "A",
                Rate = 10
            };
            var nameStartsWithDiscountRateList = new List<NameStartsWithDiscountRate>();
            nameStartsWithDiscountRateList.Add(nameStartsWithDiscountRate);
            dataSourceMoq.Setup(s => s.GetNameStartsWithDiscountRates()).Returns(nameStartsWithDiscountRateList);
            var discount = discountCalculatorByNamePrefix.CalculateDiscountGivenBeneficiaryAndCost(ben, 1000);
            Assert.AreEqual(100, discount);
        }
    }
}