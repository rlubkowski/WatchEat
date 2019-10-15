using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.Food;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodsPageViewModel : BaseViewModel
    {
        public FoodsPageViewModel()
        {
            FoodProducts = new ObservableCollection<Models.Database.FoodEntry>();
        }

        public ObservableCollection<Models.Database.FoodEntry> FoodProducts { get; private set; }

        public ICommand OpenAddProductPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new FoodPage()));
        });

        public ICommand ProductSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (Models.Database.FoodEntry)param;
            await Navigation.PushModalAsync(new NavigationPage(new FoodPage(new FoodPageViewModel(foodProduct))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var product in await DataStore.FoodEntries.Get())
                {
                    FoodProducts.Add(product);
                }

                MessagingCenter.Subscribe<FoodPageViewModel, Models.Database.FoodEntry>(this, CommandNames.AddFood, async (obj, item) =>
                {
                    FoodProducts.Add(item);
                    await DataStore.FoodEntries.Insert(item);
                });

                MessagingCenter.Subscribe<FoodPageViewModel, Models.Database.FoodEntry>(this, CommandNames.EditFood, async (obj, item) =>
                {
                    await DataStore.FoodEntries.Update(item);
                });

                MessagingCenter.Subscribe<FoodPageViewModel, Models.Database.FoodEntry>(this, CommandNames.RemoveFood, async (obj, item) =>
                {
                    FoodProducts.Remove(item);
                    await DataStore.FoodEntries.Delete(item);
                });

                await base.InitializeAsync(navigation);
            }
        }
    }
}
