using WatchEat.Enums;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class BodyFatService : IBodyFatService
    {
        //Women:
        //(1.20 x BMI) + (0.23 x Age) - 5.4 = Body Fat Percentage

        //Men:
        //(1.20 x BMI) + (0.23 x Age) - 16.2 = Body Fat Percentage

        //Women:        
        //20-40 yrs old: Underfat: under 21 percent, Healthy: 21-33 percent, Overweight: 33-39 percent, Obese: Over 39 percent
        //41-60 yrs old: Underfat: under 23 percent, Healthy: 23-35 percent, Overweight : 35-40 percent Obese: over 40 percent
        //61-79 yrs old: Underfat: under 24 percent, Healthy: 24-36 percent, Overweight: 36-42 percent, Obese: over 42 percent

        //Men:        
        //20-40 yrs old: Underfat: under 8 percent, Healthy: 8-19 percent, Overweight: 19-25 percent, Obese: over 25 percent
        //41-60 yrs old: Underfat: under 11 percent, Healthy: 11-22 percent, Overweight: 22-27 percent, Obese: over 27 percent
        //61-79 yrs old: Underfat: under 13 percent, Healthy: 13-25 percent, Overweight: 25-30 percent, Obese: over 30 percent

        public decimal EstimateBodyFatPercentage(decimal bmi, int age, Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return (1.2m * bmi) + (0.23m * age) - 16.2m;
                case Gender.Female:
                    return (1.2m * bmi) + (0.23m * age) - 5.4m;
                default:
                    return 0;                    
            }
        }

        public BodyFatLevel GetBodyFatLevel(decimal bodyFat, int age, Gender gender)
        {
            if (age >= 20 && age < 40)
            {
                if (gender == Gender.Female)
                {
                    if (bodyFat < 0.21m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.21m && bodyFat < 0.33m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.33m && bodyFat < 0.39m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.39m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
                else if (gender == Gender.Male)
                {
                    if (bodyFat < 0.08m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.08m && bodyFat < 0.19m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.19m && bodyFat < 0.25m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.25m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
            }
            else if (age >= 40 && age < 60)
            {
                if (gender == Gender.Female)
                {
                    if (bodyFat < 0.23m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.23m && bodyFat < 0.35m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.35m && bodyFat < 0.40m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.40m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
                else if (gender == Gender.Male)
                {
                    if (bodyFat < 0.11m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.11m && bodyFat < 0.22m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.22m && bodyFat < 0.27m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.27m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
            }
            else if (age >= 60 && age < 80)
            {
                if (gender == Gender.Female)
                {
                    if (bodyFat < 0.24m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.24m && bodyFat < 0.36m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.36m && bodyFat < 0.42m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.42m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
                else if (gender == Gender.Male)
                {
                    if (bodyFat < 0.13m)
                    {
                        return BodyFatLevel.Underfat;
                    }
                    else if (bodyFat >= 0.13m && bodyFat < 0.25m)
                    {
                        return BodyFatLevel.Healthy;
                    }
                    else if (bodyFat >= 0.25m && bodyFat < 0.30m)
                    {
                        return BodyFatLevel.Overweight;
                    }
                    else if (bodyFat >= 0.30m)
                    {
                        return BodyFatLevel.Obese;
                    }
                }
            }
            return BodyFatLevel.NoData;
        }
    }
}
