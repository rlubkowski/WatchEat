using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class SingleFoodProductPageViewModel : BaseViewModel
    {
        public SingleFoodProductPageViewModel()
        {
            Title = "New Food Product";
            IsEditView = false;
            Product = new FoodProduct();
        }

        public SingleFoodProductPageViewModel(FoodProduct product)
        {
            Title = "Edit Food Product";
            IsEditView = true;
            Product = product;
        }

        FoodProduct _product;
        public FoodProduct Product
        {
            get => _product;
            set { SetProperty(ref _product, value); }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditFoodProduct, Product);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddFoodProduct, Product);
            }
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DisplayAlert("Confirm Remove", "Do you want to remove selected product?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveFoodProduct, Product);
            }
            await Navigation.PopModalAsync();
        });
    }
}
