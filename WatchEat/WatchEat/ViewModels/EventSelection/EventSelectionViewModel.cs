using System;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Views.EventSelection;

namespace WatchEat.ViewModels.EventSelection
{
    public class EventSelectionViewModel : ViewModelWithChildPages
    {
        public EventSelectionViewModel(DateTime selectedDay, Action subscribeCallback, Action unsubscribeCallback)
        {
            SelectedDay = selectedDay;
            SubscribeCallback = subscribeCallback;
            UnsubscribeCallback = unsubscribeCallback;
        }

        private Action SubscribeCallback { get; set; }

        private Action UnsubscribeCallback { get; set; }

        DateTime _selectedDay;
        public DateTime SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand AddWaterEvent => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new WaterPage(new WaterViewModel(SelectedDay)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddFoodEvent => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new FoodSelectionPage(new FoodSelectionViewModel(SelectedDay)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddWeightEvent => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new WeightPage(new WeightViewModel(SelectedDay)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddActivityEvent => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new ActivitySelectionPage(new ActivitySelectionViewModel(SelectedDay)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            SubscribeCallback();
        }

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            UnsubscribeCallback();
            base.OnChildPageDisappearing(sender, e);
        }
    }
}
