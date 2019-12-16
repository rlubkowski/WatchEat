using System;
using System.Globalization;
using WatchEat.Enums;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class GoalTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is GoalType)
            {
                bool inverse = false;
                if (parameter != null)
                {
                    inverse = (bool)parameter;
                }

                switch ((GoalType)value)
                {
                    case GoalType.Maintain:
                        return inverse;
                    case GoalType.Lose:
                        return !inverse;
                    case GoalType.Gain:
                        return !inverse;
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
