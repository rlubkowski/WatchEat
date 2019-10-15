using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class EventSelectionPageViewModel : BaseViewModel
    {
        public EventSelectionPageViewModel(DateTime selectedDay)
        {
            SelectedDay = selectedDay;
        }

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
            await Navigation.PushModalAsync(new NavigationPage(new WaterPage(new WaterPageViewModel(SelectedDay))));
        });

        public ICommand AddFoodEvent => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new FoodSelectionPage(new FoodSelectionPageViewModel(SelectedDay))));
        });

        public ICommand AddWeightEvent => new AsyncCommand(async () =>
        {            
            await Navigation.PushModalAsync(new NavigationPage(new WeightPage(new WeightPageViewModel(SelectedDay))));
        });

        public ICommand AddTrainingEvent => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new ActivitySelectionPage(new ActivitySelectionPageViewModel(SelectedDay))));
        });
    }
}
