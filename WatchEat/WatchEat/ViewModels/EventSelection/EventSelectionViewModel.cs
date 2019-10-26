using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class EventSelectionViewModel : MessagesSubscribedViewModel
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
            var page = new NavigationPage(new WaterPage(new WaterViewModel(SelectedDay)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddFoodEvent => new AsyncCommand(async () =>
        {
            var page = new NavigationPage(new FoodSelectionPage(new FoodSelectionViewModel(SelectedDay)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddWeightEvent => new AsyncCommand(async () =>
        {
            var page = new NavigationPage(new WeightPage(new WeightViewModel(SelectedDay)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand AddActivityEvent => new AsyncCommand(async () =>
        {
            var page = new NavigationPage(new ActivitySelectionPage(new ActivitySelectionViewModel(SelectedDay)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnPageAppearing(object sender, EventArgs e)
        {
            SubscribeCallback();
        }

        protected override void OnPageDisappearing(object sender, EventArgs e)
        {
            UnsubscribeCallback();
            base.OnPageDisappearing(sender, e);
        }
    }
}
