using System;
using System.Globalization;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = System.Convert.ToDecimal(string.IsNullOrWhiteSpace(value.ToString()) ? 0 : value, culture);
            if (parameter != null)
            {
                return Math.Round(result, (int)parameter);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = System.Convert.ToDecimal(string.IsNullOrWhiteSpace(value.ToString()) ? 0 : value, culture);
            if (parameter != null)
            {
                return Math.Round(result, (int)parameter);
            }
            return result;
        }
    }
}
