using WatchEat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalPage : ContentPage
    {
        public JournalPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new JournalViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}