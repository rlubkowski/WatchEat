using System;

namespace WatchEat.Models
{
    public class NutritionRecommendation
    {
        public NutritionRecommendation(int totalCalories, NutritionRange carbs, NutritionRange proteins, NutritionRange fats)
        {
            TotalCalories = totalCalories;
            Carbs = carbs;
            Proteins = proteins;
            Fats = fats;
        }

        public int TotalCalories { get; private set; }

        public NutritionRange Carbs { get; private set; }

        public NutritionRange Proteins { get; private set; }

        public NutritionRange Fats { get; private set; }
    }

    public class NutritionRange
    {
        public NutritionRange(decimal gramsFrom, decimal gramsTo, int caloriesFrom, int caloriesTo)
        {
            GramsFrom = Math.Round(gramsFrom);
            GramsTo = Math.Round(gramsTo);
            CaloriesFrom = caloriesFrom;
            CaloriesTo = caloriesTo;
        }

        public decimal GramsFrom { get; private set; }

        public decimal GramsTo { get; private set; }

        public int CaloriesFrom { get; private set; }

        public int CaloriesTo { get; private set; }
    }
}
