using System.Windows.Input;
using WatchEat.Helpers;

namespace WatchEat.ViewModels.EventSelection
{
    public class ActivitySelectionPageViewModel : BaseViewModel
    {
        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand AddWaterActivity => new AsyncCommand(async () =>
        {
            
            //await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });

        public ICommand AddFoodActivity => new AsyncCommand(async () =>
        {

            //await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });

        public ICommand AddWeightActivity => new AsyncCommand(async () =>
        {

            //await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });

        public ICommand AddTrainingActivity => new AsyncCommand(async () =>
        {

            //await Navigation.PushModalAsync(new NavigationPage(new SingleFoodProductPage(new SingleFoodProductPageViewModel(foodProduct))));
        });
    }
}
