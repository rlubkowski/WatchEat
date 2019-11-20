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
            BindingContext = new EditUserViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as EditUserViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}