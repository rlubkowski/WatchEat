using Xamarin.Forms;

namespace WatchEat.Controls
{
    public class NumericInput : Entry
    {
        public static BindableProperty AllowNegativeProperty = BindableProperty.Create(nameof(AllowNegative), typeof(bool), typeof(NumericInput), false, BindingMode.TwoWay);
        public static BindableProperty AllowFractionProperty = BindableProperty.Create(nameof(AllowFraction), typeof(bool), typeof(NumericInput), false, BindingMode.TwoWay);

        public NumericInput()
        {
            this.Keyboard = Keyboard.Numeric;
        }

        public bool AllowNegative
        {
            get { return (bool)GetValue(AllowNegativeProperty); }
            set { SetValue(AllowNegativeProperty, value); }
        }

        public bool AllowFraction
        {
            get { return (bool)GetValue(AllowFractionProperty); }
            set { SetValue(AllowFractionProperty, value); }
        }
    }
}
