using Xamarin.Forms;

namespace WatchEat.Behaviors.Validation
{
    public class EntryNotEmptyValidationBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryNotEmptyValidationBehavior), false);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChangedCallback;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChangedCallback;
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChangedCallback(object sender, TextChangedEventArgs e)
        {
            IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }
    }
}
