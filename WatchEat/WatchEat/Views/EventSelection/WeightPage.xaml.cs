using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightPage : ContentPage
    {
        public WeightPage(WeightViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as WeightViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}