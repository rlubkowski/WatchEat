using System;
using WatchEat.Enums;
using WatchEat.Helpers;
using WatchEat.Models;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class NutritionService : INutritionService
    {
        //BMR
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
                default: return 0;
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

        public decimal BMRHarrisBenedict(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false)
        {
            //Imperial
            //Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
            //Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)

            //Metric
            //Women: BMR = 655 + (9.6 x weight in kg) + (1.8 x height in cm) - (4.7 x age in years)
            //Men: BMR = 66 + (13.7 x weight in kg) + (5 x height in cm) - (6.8 x age in years)

            switch (gender)
            {
                case Gender.Male:
                    return CalculateMaleHarrisBenedict(weight, height, age, imperialUnits);
                case Gender.Female:
                    return CalculateFemaleHarrisBenedict(weight, height, age, imperialUnits);
                default: return 0;
            }
        }

        public decimal BMRMifflinStJeor(decimal weight, decimal height, int age, Gender gender, bool imperialUnits = false)
        {
            //Men: 10 x weight(kg) +6.25 x height(cm) – 5 x Age +5
            //Women: 10 x weight (kg) + 6.25 x height (cm) – 5 x Age – 161

            if (imperialUnits)
            {
                height = UnitConverter.FeetToCentimeters(height);
                weight = UnitConverter.PoundsToKilograms(weight);
            }
            if (gender == Gender.Male)
            {
                return 10m * weight + 6.25m * height - 5 * age + 5;
            }
            else if (gender == Gender.Female)
            {
                return 10m * weight + 6.25m * height - 5 * age + 161;
            }
            return 0;
        }

        private int WeightChangePeriodToDays(TimePeriod timePeriod, int periodNumber)
        {
            switch (timePeriod)
            {
                case TimePeriod.Days:
                    return periodNumber;
                case TimePeriod.Weeks:
                    return periodNumber * 7;
                case TimePeriod.Months:
                    return Convert.ToInt32(periodNumber * 30.4);
                case TimePeriod.Years:
                    return periodNumber * 365;
                default: return 0;
            }
        }

        public WeightChangeEstimationResult CalculateWeightChangeEstimation(int age, decimal height, decimal weight, Gender gender, ActivityFactor activityFactor, TimePeriod timePeriod, int periodNumber, GoalType goalType, decimal weightLoseGain, bool imperialUnits = false)
        {
            if (imperialUnits)
            {
                height = UnitConverter.FeetToCentimeters(height);
                weight = UnitConverter.PoundsToKilograms(weight);
            }

            var bmr = BMRHarrisBenedict(weight, height, age, gender, imperialUnits);            

            var dailyCalorieNeed = GetDailyCaloriesEstimation(bmr, activityFactor);

            var totalGoalDays = WeightChangePeriodToDays(timePeriod, periodNumber);
            var totalCalories = UnitConverter.KilogramsToPounds(weightLoseGain) * 3500;

            var dailyCaloriesChange = totalCalories / totalGoalDays;

            if (dailyCaloriesChange > 3500m)
            {
                return new WeightChangeEstimationResult();
            }

            if (goalType == GoalType.Lose)
            {
                var caloriesToReachGoal = dailyCalorieNeed - dailyCaloriesChange;
                var caloriesToMaintainGoal = 
                    GetDailyCaloriesEstimation(bmr, activityFactor) * (UnitConverter.KilogramsToPounds(weight) -  UnitConverter.KilogramsToPounds(weightLoseGain)) / UnitConverter.KilogramsToPounds(weight);
                return new WeightChangeEstimationResult(Convert.ToInt32(caloriesToReachGoal), Convert.ToInt32(caloriesToMaintainGoal));
            }
            else if (goalType == GoalType.Gain)
            {
                var caloriesToReachGoal = dailyCalorieNeed + dailyCaloriesChange;
                var caloriesToMaintainGoal =
                    GetDailyCaloriesEstimation(bmr, activityFactor) * (UnitConverter.KilogramsToPounds(weight) + UnitConverter.KilogramsToPounds(weightLoseGain)) / UnitConverter.KilogramsToPounds(weight);
                return new WeightChangeEstimationResult(Convert.ToInt32(caloriesToReachGoal), Convert.ToInt32(caloriesToMaintainGoal));
            }
            else if (goalType == GoalType.Maintain)
            {
                return new WeightChangeEstimationResult(Convert.ToInt32(dailyCalorieNeed), Convert.ToInt32(dailyCalorieNeed));
            }
            return new WeightChangeEstimationResult();
        }

        private decimal CalculateFemaleHarrisBenedict(decimal weight, decimal height, int age, bool imperialUnits)
        {
            if (imperialUnits)
            {
                return 655m + (4.35m * weight) + (4.7m * height) - (4.7m * age);
            }
            else
            {
                return 665m + (9.6m * weight) + (1.8m * height) - (4.7m * age);
            }
        }

        private decimal CalculateMaleHarrisBenedict(decimal weight, decimal height, int age, bool imperialUnits)
        {
            if (imperialUnits)
            {
                return 66m + (6.23m * weight) + (12.7m * height) - (6.8m * age);
            }
            else
            {
                return 66m + (13.7m * weight) + (5m * height) - (6.8m * age);
            }
        }

        public decimal CalculateDailyWaterIntake(decimal weight, bool imperialUnits = false)
        {
            if(!imperialUnits)
            {
                weight = UnitConverter.KilogramsToPounds(weight);
            }
            
            var result = weight / 2;

            return imperialUnits ? result : UnitConverter.OuncesToMililiters(result);
        }
    }
}
