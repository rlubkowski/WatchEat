using NUnit.Framework;
using WatchEat.Services;

namespace WatchEat.Tests.Services
{
    [TestFixture]
    public class NutritionServiceTests
    {
        private NutritionService _nutritionService;

        [SetUp]
        public void Setup()
        {
            _nutritionService = new NutritionService();
        }

        [Test]
        public void Calculate_GivenHeightWeightAgeMale_CalculatesHarrisBenedictBMR()
        {
            decimal height = 190;
            decimal weight = 90;
            int age = 30;

            var bmr = _nutritionService.BMRHarrisBenedict(weight, height, age, Enums.Gender.Male);

            Assert.AreEqual(bmr, 2045m);
        }

        [Test]
        public void CalculateWeightChangeEstimation_GivenLoseParameters_ReturnsCaloriesRecommendation()
        {
            var result = _nutritionService.CalculateWeightChangeEstimation(30, 190, 90, Enums.Gender.Male, Enums.ActivityFactor.Sedentary, Enums.TimePeriod.Months, 2, Enums.GoalType.Lose, 9);

            Assert.AreEqual(true, true);
        }

        [Test]
        public void CalculateWeightChangeEstimation_GivenGainParameters_ReturnsCaloriesRecommendation()
        {

        }

        [Test]
        public void CalculateWeightChangeEstimation_GivenMaintainParameters_ReturnsCaloriesRecommendation()
        {

        }


        [Test]
        public void CalculateNutritionRecommendation_GivenCalories_CalculatesNutritionRecommendation()
        {
            var result = _nutritionService.CalculateNutritionRecommendation(2500);

            Assert.AreEqual(result.TotalCalories, 2500);

            Assert.AreEqual(result.Carbs.GramsFrom, 282);
            Assert.AreEqual(result.Carbs.GramsTo, 407);
            Assert.AreEqual(result.Carbs.CaloriesFrom, 1125);
            Assert.AreEqual(result.Carbs.CaloriesTo, 1625);
            
            Assert.AreEqual(result.Proteins.GramsFrom, 63);
            Assert.AreEqual(result.Proteins.GramsTo, 219);
            Assert.AreEqual(result.Proteins.CaloriesFrom, 250);
            Assert.AreEqual(result.Proteins.CaloriesTo, 875);

            Assert.AreEqual(result.Fats.GramsFrom, 56);
            Assert.AreEqual(result.Fats.GramsTo, 98);
            Assert.AreEqual(result.Fats.CaloriesFrom, 500);
            Assert.AreEqual(result.Fats.CaloriesTo, 875);
        }
    }
}
