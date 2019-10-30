using System;
using System.Globalization;
using Xamarin.Forms;

namespace WatchEat.Converters
{
    public class SelectionChangedEventArgsToCurrentSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectionChangedEventArgs;
            return eventArgs.CurrentSelection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
