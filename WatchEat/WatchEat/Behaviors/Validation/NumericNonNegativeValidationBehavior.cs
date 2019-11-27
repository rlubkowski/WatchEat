using WatchEat.Controls;
using Xamarin.Forms;

namespace WatchEat.Behaviors.Validation
{
    public class NumericNonNegativeValidationBehavior : Behavior<NumericEntry>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(NumericNonNegativeValidationBehavior), false);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
        
        protected override void OnAttachedTo(NumericEntry bindable)
        {
            bindable.TextChanged += OnTextChangedCallback;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(NumericEntry bindable)
        {
            bindable.TextChanged -= OnTextChangedCallback;
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChangedCallback(object sender, TextChangedEventArgs e)
        {
            decimal parseResult = 0;
            bool result = decimal.TryParse(e.NewTextValue, out parseResult);
            IsValid = result && parseResult != 0 && parseResult > 0 ? true : false;
        }        
    }
}
