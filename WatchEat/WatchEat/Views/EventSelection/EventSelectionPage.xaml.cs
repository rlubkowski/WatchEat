using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSelectionPage : ContentPage
    {
        public EventSelectionPage(EventSelectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as EventSelectionViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}