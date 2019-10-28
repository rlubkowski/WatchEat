using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Food
{
    public class FoodViewModel : BaseViewModel
    {
        public FoodViewModel()
        {
            Title = AppResource.NewFood;
            IsEditView = false;
            Food = new FoodEntry();
        }

        public FoodViewModel(FoodEntry food)
        {
            Title = AppResource.EditFood;
            IsEditView = true;
            Food = food;
        }

        FoodEntry _food;
        public FoodEntry Food
        {
            get => _food;
            set { SetProperty(ref _food, value); }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditFood, Food);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddFood, Food);
            }
            
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DialogService.DisplayAlert("Confirm Remove", "Do you want to remove selected product?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveFood, Food);
            }
            await Navigation.PopModalAsync();
        });
    }
}
