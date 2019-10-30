using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Views.Food;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodsViewModel : MessagesSubscribedViewModel
    {
        public FoodsViewModel()
        {
            Foods = new ObservableCollection<FoodEntry>();
        }

        public ObservableCollection<FoodEntry> Foods { get; private set; }

        public ICommand OpenAddFoodPage => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new FoodPage());
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand FoodSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (FoodEntry)param;
            var page = new StyledNavigationPage(new FoodPage(new FoodViewModel(foodProduct)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<FoodViewModel, FoodEntry>(this, CommandNames.AddFood, async (obj, item) =>
            {
                Foods.Add(item);
                await DataStore.FoodEntries.Insert(item);
            });

            MessagingCenter.Subscribe<FoodViewModel, FoodEntry>(this, CommandNames.EditFood, async (obj, item) =>
            {
                await DataStore.FoodEntries.Update(item);
            });

            MessagingCenter.Subscribe<FoodViewModel, FoodEntry>(this, CommandNames.RemoveFood, async (obj, item) =>
            {
                Foods.Remove(item);
                await DataStore.FoodEntries.Delete(item);
            });
        }

        protected override void OnPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<FoodViewModel, FoodEntry>(this, CommandNames.AddFood);
            MessagingCenter.Unsubscribe<FoodViewModel, FoodEntry>(this, CommandNames.EditFood);
            MessagingCenter.Unsubscribe<FoodViewModel, FoodEntry>(this, CommandNames.RemoveFood);
            base.OnPageDisappearing(sender, e);
        }

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);            

            foreach (var product in await DataStore.FoodEntries.Get())
            {
                Foods.Add(product);
            }
        }
    }
}
