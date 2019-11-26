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
            BindingContext = new UserViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as UserViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}