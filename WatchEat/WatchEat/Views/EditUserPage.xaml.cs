using WatchEat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        public EditUserPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new EditUserViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}