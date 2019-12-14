using System;
using WatchEat.Enums;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class NutritionService : INutritionService
    {
        //BMR: Harris - Benedict or Mifflin - St.Jeor
        //If you are sedentary(little or no exercise) : Calorie-Calculation = BMR x 1.2
        //If you are lightly active(light exercise/sports 1-3 days/week) : Calorie-Calculation = BMR x 1.375
        //If you are moderatetely active(moderate exercise/sports 3-5 days/week) : Calorie-Calculation = BMR x 1.55
        //If you are very active(hard exercise/sports 6-7 days a week) : Calorie-Calculation = BMR x 1.725
        //If you are extra active(very hard exercise/sports & physical job or 2x training) : Calorie-Calculation = BMR x 1.9
        public decimal GetDailyCaloriesEstimation(decimal bmr, ActivityFactor activityFactor)
        {
            switch (activityFactor)
            {
                case ActivityFactor.Sedentary:
                    return bmr * 1.2m;
                case ActivityFactor.LightlyActive:
                    return bmr * 1.375m;
                case ActivityFactor.ModeratetelyActive:
                    return bmr * 1.55m;
                case ActivityFactor.VeryActive:
                    return bmr * 1.725m;
                case ActivityFactor.ExtraActive:
                    return bmr * 1.9m;
                default:
                    return 0;
            }
        }

        public NutritionRecommendation CalculateNutritionRecommendation(int calories)
        {
            var carbsCaloriesFrom = Math.Ceiling(0.45m * calories);
            var carbsCaloriesTo = Math.Ceiling(0.65m * calories);
            var carbsGramsFrom = Math.Ceiling(carbsCaloriesFrom / 4);
            var carbsGramsTo = Math.Ceiling(carbsCaloriesTo / 4);
            var proteinsCaloriesFrom = Math.Ceiling(0.1m * calories);
            var proteinsCaloriesTo = Math.Ceiling(0.35m * calories);
            var proteinsGramsFrom = Math.Ceiling(proteinsCaloriesFrom / 4);
            var proteinsGramsTo = Math.Ceiling(proteinsCaloriesTo / 4);
            var fatsCaloriesFrom = Math.Ceiling(0.2m * calories);
            var fatsCaloriesTo = Math.Ceiling(0.35m * calories);
            var fatsGramsFrom = Math.Ceiling(fatsCaloriesFrom / 9);
            var fatsGramsTo = Math.Ceiling(fatsCaloriesTo / 9);

            var carbs = new NutritionRange(carbsGramsFrom, carbsGramsTo, Convert.ToInt32(carbsCaloriesFrom), Convert.ToInt32(carbsCaloriesTo));
            var proteins = new NutritionRange(proteinsGramsFrom, proteinsGramsTo, Convert.ToInt32(proteinsCaloriesFrom), Convert.ToInt32(proteinsCaloriesTo));
            var fats = new NutritionRange(fatsGramsFrom, fatsGramsTo, Convert.ToInt32(fatsCaloriesFrom), Convert.ToInt32(fatsCaloriesTo));
            return new NutritionRecommendation(calories, carbs, proteins, fats);
        }
    }
}
