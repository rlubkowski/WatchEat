using WatchEat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalPage : ContentPage
    {
        public GoalPage()
        {
            InitializeComponent();
            BindingContext = new GoalViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as GoalViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}