using System;
using System.Collections.Generic;
using System.Text;
using WatchEat.Enums;
using WatchEat.Services.Interfaces;

namespace WatchEat.Services
{
    public class IBWService : IIBWService
    {
        //The Broca Index
        //Men: Ideal Body Weight(kg) = [Height(cm) - 100] - ([Height(cm) - 100] x 10%)
        //Women: Ideal Body Weight(kg) = [Height(cm) - 100] + ([Height(cm) - 100] x 15%)

        //The Devine formula
        //Men: Ideal Body Weight(kg) = 50 kg + 2.3 kg per inch over 5 feet.
        //Women: Ideal Body Weight(kg) = 45.5 kg + 2.3 kg per inch over 5 feet.

        //Robinson Formula
        //Men: Ideal Body Weight(kg) = 52 kg + 1.9 kg per inch over 5 feet.
        //Women: Ideal Body Weight(kg) = 49 kg + 1.7 kg per inch over 5 feet.

        //The Miller formula:
        //Men: Ideal Body Weight(kg) = 56.2 kg + 1.41 kg per inch over 5 feet.
        //Women: Ideal Body Weight(kg) = 53.1 kg + 1.36 kg per inch over 5 feet.

        //The Hamwi formula:
        //Men: Ideal Body Weight(in kilograms) = 48 kg + 2.7 kg for each inch over 5 feet
        //Women: Ideal Body Weight(in kilograms) = 45.5 kg + 2.2 kg for each inch over 5 feet.
        //If the wrist is 7 inches, the IBW remains the way it is. However, if the wrist size is more or less than 7 inches, you add or subtract 10% of ideal body weight respectively.

        //Lemmens formula:
        //Ideal Body Weight(kg) = 22 x height^2 (meter)

        public decimal CalculateBroca(decimal height, Gender gender, bool imperialUnits = false)
        {
            if (imperialUnits)
            {
                height = height / 0.39370m;
            }
            if (gender == Gender.Male)
            {
                return (height - 100m) - ((height - 100m) * 0.10m);
            }
            else if (gender == Gender.Female)
            {
                return (height - 100m) + ((height - 100m) * 0.15m);
            }
            return 0;
        }

        public decimal CalculateDevine(decimal height, Gender gender, bool imperialUnits = false)
        {
            if (!imperialUnits)
            {
                height = height * 0.39370m;
            }
            if (gender == Gender.Male)
            {
                return (height - 100m) - ((height - 100m) * 0.10m);
            }
            else if (gender == Gender.Female)
            {
                return (height - 100m) + ((height - 100m) * 0.15m);
            }
            return 0;
        }

        public decimal CalculateHamwi(decimal height, Gender gender, bool imperialUnits = false)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateLemmens(decimal height, bool imperialUnits = false)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateMiller(decimal height, Gender gender, bool imperialUnits = false)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateRobinson(decimal height, Gender gender, bool imperialUnits = false)
        {
            throw new NotImplementedException();
        }
    }
}
