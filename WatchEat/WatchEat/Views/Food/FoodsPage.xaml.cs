using WatchEat.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodsPage : ContentPage
    {
        public FoodsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new FoodsPageViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}