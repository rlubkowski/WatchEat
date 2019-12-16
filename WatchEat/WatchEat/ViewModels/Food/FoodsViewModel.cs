using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models.Database;
using WatchEat.Resources;
using WatchEat.Views.Food;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodsViewModel : ViewModelWithChildPages
    {
        public FoodsViewModel()
        {
            Foods = new ObservableCollection<FoodEntry>();
            Title = AppResource.Foods;
        }

        public ObservableCollection<FoodEntry> Foods { get; private set; }

        public ICommand OpenAddFoodPage => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new FoodPage());
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand FoodSelected => new AsyncCommand(async (param) =>
        {
            var foodProduct = (FoodEntry)param;
            var page = new StyledNavigationPage(new FoodPage(new FoodViewModel(foodProduct)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<FoodViewModel, FoodEntry>(this, CommandNames.AddFood, (obj, item) =>
            {
                Foods.Add(item);
            });

            MessagingCenter.Subscribe<FoodViewModel, FoodEntry>(this, CommandNames.RemoveFood, (obj, item) =>
            {
                Foods.Remove(item);
            });
        }

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<FoodViewModel, FoodEntry>(this, CommandNames.AddFood);            
            MessagingCenter.Unsubscribe<FoodViewModel, FoodEntry>(this, CommandNames.RemoveFood);
            base.OnChildPageDisappearing(sender, e);
        }

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
            Foods.Clear();
            Foods.AddRange(await DataStore.FoodEntries.Get());
        }
    }
}
