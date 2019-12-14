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
