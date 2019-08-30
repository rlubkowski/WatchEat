using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchEat.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodProductsPage : ContentPage
    {
        public FoodProductsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var viewModel = new FoodProductsPageViewModel();
            await viewModel.InitializeAsync(Navigation);
            BindingContext = viewModel;
            base.OnAppearing();
        }
    }
}