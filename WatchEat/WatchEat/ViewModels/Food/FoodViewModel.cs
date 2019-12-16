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

        bool _isNameValid;
        public bool IsNameValid
        {
            get => _isNameValid;
            set { SetProperty(ref _isNameValid, value); }
        }

        bool _isCaloriesValid;
        public bool IsCaloriesValid
        {
            get => _isCaloriesValid;
            set { SetProperty(ref _isCaloriesValid, value); }
        }

        public bool IsValid
        {
            get => IsCaloriesValid && IsNameValid;
        }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if(!IsValid)
            {
                await DialogService.DisplayAlert(AppResource.Validation, AppResource.ValidationNameCalories, AppResource.Cancel);
                return;
            }

            if (IsEditView)
            {   
                await DataStore.FoodEntries.Update(Food);                
            }
            else
            {
                await DataStore.FoodEntries.Insert(Food);
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
            if (await DialogService.DisplayAlert(AppResource.ConfirmRemove, AppResource.DoYouWantToRemoveSelectedItem, AppResource.Yes, AppResource.No))
            {
                await DataStore.FoodEntries.Delete(Food);
                MessagingCenter.Send(this, CommandNames.RemoveFood, Food);
            }
            await Navigation.PopModalAsync();
        });
    }
}
