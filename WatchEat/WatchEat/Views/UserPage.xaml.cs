using WatchEat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new UserViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}