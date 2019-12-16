using WatchEat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditGoalPage : ContentPage
    {
        public EditGoalPage()
        {
            InitializeComponent();
            BindingContext = new EditGoalViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as EditGoalViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}