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
            BindingContext = new FoodsViewModel();
        }        

        protected override async void OnAppearing()
        {
            await (BindingContext as FoodsViewModel).InitializeAsync(Navigation);
            base.OnAppearing();
        }
    }
}