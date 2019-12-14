namespace WatchEat.Helpers
{
    public static class UnitConverter
    {
        public static decimal CentimetersToInches(decimal centimeters)
        {
            return centimeters * 0.39370m;
        }

        public static decimal InchesToCentimeters(decimal inches)
        {
            return inches / 0.39370m;
        }

        public static decimal KilogramsToPounds(decimal kilograms)
        {
            return kilograms * 2.2046m;
        }

        public static decimal PoundsToKilograms(decimal pounds)
        { 
            return pounds / 2.2046m;
        }

        public static decimal InchesToFeet(decimal inches)
        {
            return inches * 0.083333m;
        }

        public static decimal FeetToInches(decimal feet)
        {
            return feet / 0.083333m;
        }

        public static decimal CentimetersToFeet(decimal centimeters)
        {
            return centimeters * 0.032808m;
        }

        public static decimal FeetToCentimeters(decimal feet)
        {
            return feet / 0.032808m;
        }

        public static decimal MililitersToOunces(decimal mililiters)
        {
            return mililiters * 0.033814m;
        }

        public static decimal OuncesToMililiters(decimal ounces)
        {
            return ounces / 0.033814m;
        }
    }
}
