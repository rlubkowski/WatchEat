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
        public void Calculate_GivenHeightWeightAgeMale_CalculatesHarrisBenedictBMR()
        {
            decimal height = 190;
            decimal weight = 90;
            int age = 30;


            var bmr = _bmrService.BMRHarrisBenedict(weight, height, age, Enums.Gender.Male);

            Assert.AreEqual(bmr, 2045m);
        }

        [Test]
        public void Calculate_GivenHeightWeightAndAgeImperial_CalculatesBMR()
        {

        }
    }
}
