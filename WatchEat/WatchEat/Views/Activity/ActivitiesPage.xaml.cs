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
            BindingContext = new ActivitiesViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as ActivitiesViewModel).InitializeAsync(Navigation);            
            base.OnAppearing();
        }
    }
}