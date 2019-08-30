using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Views.Food;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodProductsPageViewModel : BaseViewModel
    {
        public FoodProductsPageViewModel()
        {
            FoodProducts = new ObservableCollection<FoodProduct>();
        }

        public ObservableCollection<FoodProduct> FoodProducts { get; private set; }

        public ICommand OpenAddProductPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage()));
        });

        public ICommand ProductSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (FoodProduct)param;
            await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            foreach (var product in await DataStore.FoodProducts.Get())
            {
                FoodProducts.Add(product);
            }

            MessagingCenter.Subscribe<SingleFoodProductPageViewModel, FoodProduct>(this, CommandNames.AddFoodProduct, async (obj, item) =>
            {
                FoodProducts.Add(item);
                await DataStore.FoodProducts.Insert(item);
            });

            MessagingCenter.Subscribe<SingleFoodProductPageViewModel, FoodProduct>(this, CommandNames.EditFoodProduct, async (obj, item) =>
            {
                await DataStore.FoodProducts.Update(item);
            });

            MessagingCenter.Subscribe<SingleFoodProductPageViewModel, FoodProduct>(this, CommandNames.RemoveFoodProduct, async (obj, item) =>
            {
                FoodProducts.Remove(item);
                await DataStore.FoodProducts.Delete(item);
            });

            await base.InitializeAsync(navigation);
        }
    }
}
