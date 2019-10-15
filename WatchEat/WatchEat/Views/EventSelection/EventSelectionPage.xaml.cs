using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSelectionPage : ContentPage
    {
        public EventSelectionPage(EventSelectionPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as EventSelectionPageViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}