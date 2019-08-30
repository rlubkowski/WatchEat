using System;
using System.Globalization;
using Xamarin.Forms;

namespace WatchEat.Views.Controls
{
    public class NumericEntry : Entry
    {
        public NumericEntry()
        {
            Keyboard = Keyboard.Numeric;
            Focused += OnFocused;
            Unfocused += OnUnfocused;
        }

        public static readonly BindableProperty NumericValueProperty = BindableProperty.Create(
            "NumericValue",
            typeof(decimal?),
            typeof(NumericEntry),
            null,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                SetDisplayFormat((NumericEntry)bindable);
            }
        );

        public static readonly BindableProperty NumericValueFormatProperty = BindableProperty.Create(
            "NumericValueFormat",
            typeof(string),
            typeof(NumericEntry),
            "N2",
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                SetDisplayFormat((NumericEntry)bindable);
            }
        );

        private void OnFocused(object sender, FocusEventArgs e)
        {
            SetEditFormat(this);
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            var numberFormant = CultureInfo.InvariantCulture.NumberFormat;

            if (decimal.TryParse(Text, NumberStyles.Number, CultureInfo.DefaultThreadCurrentCulture, out decimal numericValue))
            {
                int round = Convert.ToInt32(NumericValueFormat.Substring(1));
                NumericValue = Math.Round(numericValue, round);
            }
            else
            {
                NumericValue = null;
            }

            SetDisplayFormat(this);
        }

        public decimal? NumericValue
        {
            get { return (decimal?)GetValue(NumericValueProperty); }
            set { SetValue(NumericValueProperty, value); }
        }

        public string NumericValueFormat
        {
            get { return (string)GetValue(NumericValueFormatProperty) ?? "N2"; }
            set
            {
                var _value = string.IsNullOrWhiteSpace(value) ? "N2" : value;
                SetValue(NumericValueFormatProperty, value);
            }
        }

        private static void SetDisplayFormat(NumericEntry textBox)
        {
            if (textBox.NumericValue.HasValue)
            {
                textBox.Text = textBox.NumericValue.Value.ToString(textBox.NumericValueFormat, CultureInfo.DefaultThreadCurrentCulture);
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        private static void SetEditFormat(NumericEntry textBox)
        {
            if (textBox.NumericValue.HasValue)
            {
                textBox.Text = textBox.NumericValue.Value.ToString(textBox.NumericValueFormat, CultureInfo.DefaultThreadCurrentCulture);
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }
    }
}
