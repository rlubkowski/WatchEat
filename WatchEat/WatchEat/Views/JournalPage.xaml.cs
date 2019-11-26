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
            BindingContext = new JournalViewModel();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as JournalViewModel).InitializeAsync(Navigation);            
            base.OnAppearing();
        }
    }
}