using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalEntryEditPage : ContentPage
    {
        public JournalEntryEditPage(JournalEntryEditViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as JournalEntryEditViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}