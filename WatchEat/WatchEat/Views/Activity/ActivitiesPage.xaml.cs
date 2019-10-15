using WatchEat.ViewModels.Activity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPage : ContentPage
    {
        public ActivitiesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new ActivitiesPageViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}