using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitySelectionPage : ContentPage
    {
        public ActivitySelectionPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel =  new ActivitySelectionPageViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}