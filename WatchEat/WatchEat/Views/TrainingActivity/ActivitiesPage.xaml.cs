using WatchEat.ViewModels.TrainingActivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.TrainingActivity
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
            var viewModel = new TrainingActivitiesPageViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}