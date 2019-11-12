using System;
using System.Collections.Generic;
using System.Text;

namespace WatchEat.Services.Interfaces
{
    public interface ICaloriesService
    {
        //Harris Benedict Equation
        //If you are sedentary(little or no exercise) : Calorie-Calculation = BMR x 1.2
        //If you are lightly active(light exercise/sports 1-3 days/week) : Calorie-Calculation = BMR x 1.375
        //If you are moderatetely active(moderate exercise/sports 3-5 days/week) : Calorie-Calculation = BMR x 1.55
        //If you are very active(hard exercise/sports 6-7 days a week) : Calorie-Calculation = BMR x 1.725
        //If you are extra active(very hard exercise/sports & physical job or 2x training) : Calorie-Calculation = BMR x 1.9


        //https://calculator.me/planning/weight-loss.php
    }
}
