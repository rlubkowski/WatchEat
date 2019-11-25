using WatchEat.Controls;
using Xamarin.Forms;

namespace WatchEat.Behaviors.Validation
{
    public class WeightValidationBehavior : Behavior<NumericEntry>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(WeightValidationBehavior), false, BindingMode.OneWayToSource);

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
            
        }        
    }
}
