using System;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.EventSelection;
using Xamarin.Forms;

namespace WatchEat.ViewModels.EventSelection
{
    public class ActivitySelectionPageViewModel : BaseViewModel
    {
        public ActivitySelectionPageViewModel(DateTime selectedDay)
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

        public ICommand AddWaterActivity => new AsyncCommand(async () =>
        {            
            await Navigation.PushModalAsync(new NavigationPage(new WaterActivityPage(new WaterActivityPageViewModel(SelectedDay))));
        });

        public ICommand AddFoodActivity => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new FoodSelectionPage(new FoodSelectionPageViewModel(SelectedDay))));
        });

        public ICommand AddWeightActivity => new AsyncCommand(async () =>
        {            
            await Navigation.PushModalAsync(new NavigationPage(new WeightActivityPage(new WeightActivityPageViewModel(SelectedDay))));
        });

        public ICommand AddTrainingActivity => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new TrainingActivitySelectionPage(new TrainingActivitySelectionPageViewModel(SelectedDay))));
        });
    }
}
