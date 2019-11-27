using NUnit.Framework;
using WatchEat.Enums;
using WatchEat.Services;

namespace WatchEat.Tests.Services
{
    [TestFixture]
    public class BMIServiceTests
    {
        private BMIService _bmiService;

        [SetUp]
        public void Setup()
        {
            _bmiService = new BMIService();
        }

        [Test]
        public void Calculate_GivenHeightAndWeight_CalculatesBMI()
        {
            decimal height = 190;
            decimal weight = 90;
            var calculationResult = _bmiService.Calculate(weight, height);
            Assert.AreEqual(calculationResult.BMIValue, 24.93m);
            Assert.AreEqual(calculationResult.BMIFactor, BMI.HealthyWeight);
        }

        [Test]
        public void Calculate_GivenHeightAndWeightImperial_CalculatesBMI()
        {
            decimal height = 74.80314961m;
            decimal weight = 198.4161m;
            var calculationResult = _bmiService.Calculate(weight, height, true);
            Assert.AreEqual(calculationResult.BMIValue, 24.93m);
            Assert.AreEqual(calculationResult.BMIFactor, BMI.HealthyWeight);
        }
    }
}
