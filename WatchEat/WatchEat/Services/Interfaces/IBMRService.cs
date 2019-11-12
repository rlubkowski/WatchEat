using System;
using System.Collections.Generic;
using System.Text;

namespace WatchEat.Services.Interfaces
{
    public interface IBMRService
    {
        //Imperial
        //Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
        //Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)

        //Metric
        //Women: BMR = 655 + (9.6 x weight in kg) + (1.8 x height in cm) - (4.7 x age in years)
        //Men: BMR = 66 + (13.7 x weight in kg) + (5 x height in cm) - (6.8 x age in years)

    }
}
