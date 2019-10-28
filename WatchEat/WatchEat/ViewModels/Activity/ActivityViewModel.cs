using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        public ActivityViewModel()
        {
            Title = "New Activity";
            IsEditView = false;
            Activity = new ActivityEntry();
        }

        public ActivityViewModel(ActivityEntry activity)
        {
            Title = "Edit Activity";
            IsEditView = true;
            Activity = activity;
        }

        ActivityEntry _activity;
        public ActivityEntry Activity
        {
            get => _activity;
            set { SetProperty(ref _activity, value); }
        }

        public bool IsEditView { get; private set; }

        public ICommand Save => new AsyncCommand(async () =>
        {
            if (IsEditView)
            {
                MessagingCenter.Send(this, CommandNames.EditActivity, Activity);
            }
            else
            {
                MessagingCenter.Send(this, CommandNames.AddActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });

        public ICommand Cancel => new AsyncCommand(async () =>
        {
            await Navigation.PopModalAsync();
        });

        public ICommand Remove => new AsyncCommand(async () =>
        {
            if (await DialogService.DisplayAlert("Confirm Remove", "Do you want to remove selected activity?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });
    }
}
