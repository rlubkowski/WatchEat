using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaterPage : ContentPage
    {
        public WaterPage(WaterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as WaterViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}