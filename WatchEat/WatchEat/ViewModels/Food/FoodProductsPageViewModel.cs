using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.Food;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodPageViewModel : BaseViewModel
    {
        public FoodPageViewModel()
        {
            FoodProducts = new ObservableCollection<Models.Database.Food>();
        }

        public ObservableCollection<Models.Database.Food> FoodProducts { get; private set; }

        public ICommand OpenAddProductPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage()));
        });

        public ICommand ProductSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (Models.Database.Food)param;
            await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var product in await DataStore.FoodProducts.Get())
                {
                    FoodProducts.Add(product);
                }

                MessagingCenter.Subscribe<SingleFoodProductPageViewModel, Models.Database.Food>(this, CommandNames.AddFood, async (obj, item) =>
                {
                    FoodProducts.Add(item);
                    await DataStore.FoodProducts.Insert(item);
                });

                MessagingCenter.Subscribe<SingleFoodProductPageViewModel, Models.Database.Food>(this, CommandNames.EditFood, async (obj, item) =>
                {
                    await DataStore.FoodProducts.Update(item);
                });

                MessagingCenter.Subscribe<SingleFoodProductPageViewModel, Models.Database.Food>(this, CommandNames.RemoveFood, async (obj, item) =>
                {
                    FoodProducts.Remove(item);
                    await DataStore.FoodProducts.Delete(item);
                });

                await base.InitializeAsync(navigation);
            }
        }
    }
}
