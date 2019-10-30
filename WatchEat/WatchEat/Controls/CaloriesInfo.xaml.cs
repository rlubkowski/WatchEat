using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaloriesInfo : ContentView
    {
        public CaloriesInfo()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty BurnedProperty = BindableProperty.Create(nameof(Burned),
                                                                                         typeof(decimal),
                                                                                         typeof(CaloriesInfo),
                                                                                         default(decimal),
                                                                                         BindingMode.OneWay);
        public decimal Burned
        {
            get { return (decimal)GetValue(BurnedProperty); }
            set { SetValue(BurnedProperty, value); }
        }

        public static readonly BindableProperty ConsumedProperty = BindableProperty.Create(nameof(Consumed),
                                                                                           typeof(decimal),
                                                                                           typeof(CaloriesInfo),
                                                                                           default(decimal),
                                                                                           BindingMode.OneWay);
        public decimal Consumed
        {
            get { return (decimal)GetValue(ConsumedProperty); }
            set { SetValue(ConsumedProperty, value); }
        }

        public static readonly BindableProperty RecommendedProperty = BindableProperty.Create(nameof(Recommended),
                                                                                              typeof(decimal),
                                                                                              typeof(CaloriesInfo),
                                                                                              default(decimal),
                                                                                              BindingMode.OneWay);
        public decimal Recommended
        {
            get { return (decimal)GetValue(RecommendedProperty); }
            set { SetValue(RecommendedProperty, value); }
        }
    }
}