using System;
using System.Globalization;
using WatchEat.Helpers.MethodExtensions;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class EnumToTranslateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (value as Enum).GetTranslation();                
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
