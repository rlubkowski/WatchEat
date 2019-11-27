using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WatchEat.Services;

namespace WatchEat.Tests.Services
{
    [TestFixture]
    public class BMRServiceTests
    {
        private BMRService _bmrService;

        [SetUp]
        public void Setup()
        {
            _bmrService = new BMRService();
        }

        [Test]
        public void Calculate_GivenHeightWeightAndAge_CalculatesBMR()
        {
            decimal height = 190;
            decimal weight = 90;
            int age = 30;
            var calculationResult = _bmrService.Calculate(weight, height);
            Assert.AreEqual(calculationResult.BMIValue, 24.93m);
            Assert.AreEqual(calculationResult.BMIFactor, BMI.HealthyWeight);
        }

        [Test]
        public void Calculate_GivenHeightWeightAndAgeImperial_CalculatesBMR()
        {

        }
    }
}
