using System;
using System.Globalization;
using WatchEat.Enums;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class GenderToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Gender)value == (Gender)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (Gender)parameter;
            }
            else
            {
                switch ((Gender)parameter)
                {
                    case Gender.Male:
                        return Gender.Female;
                    case Gender.Female:
                        return Gender.Male;
                    default:
                        return default(Gender);
                }
            }
        }
    }
}
