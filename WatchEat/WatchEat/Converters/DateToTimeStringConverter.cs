using System;
using System.Globalization;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class DateToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToString(culture.DateTimeFormat.ShortTimePattern);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                DateTime resultValue;
                var result = DateTime.TryParse((string)value, culture, DateTimeStyles.None, out resultValue);
                if (result)
                {
                    return resultValue;
                }
            }
            return DateTime.Now;
        }
    }
}
