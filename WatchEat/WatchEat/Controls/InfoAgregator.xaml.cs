
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoAgregator : ContentView
    {
        public InfoAgregator()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                      typeof(string),
                                                                                      typeof(InfoAgregator),
                                                                                      string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
                                                                                        typeof(decimal),
                                                                                        typeof(InfoAgregator),
                                                                                        default(decimal));

        public decimal Value
        {
            get { return (decimal)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}