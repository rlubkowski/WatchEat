using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using WatchEat.Models;

namespace WatchEat.Services.Interfaces
{
    public interface IBMIService
    {
        //Metric BMI Formula
        //BMI = weight(kg) / [height(m)]2


        //Imperial BMI Formula
        //BMI = 703 × weight(lbs) / [height(in)]2
        BMICalculationResult Calculate(Mass weight, Length height);

   
    }
}
