using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodSelectionPage : ContentPage
    {
        public FoodSelectionPage(FoodSelectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as FoodSelectionViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}