using System.Windows.Input;
using WatchEat.Helpers;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivityPageViewModel : BaseViewModel
    {
        public ActivityPageViewModel()
        {
            Title = "New Activity";
            IsEditView = false;
            Activity = new Models.Database.ActivityEntry();
        }

        public ActivityPageViewModel(Models.Database.ActivityEntry activity)
        {
            Title = "Edit Activity";
            IsEditView = true;
            Activity = activity;
        }

        Models.Database.ActivityEntry _activity;
        public Models.Database.ActivityEntry Activity
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
            if (await DisplayAlert("Confirm Remove", "Do you want to remove selected activity?", "Yes", "No"))
            {
                MessagingCenter.Send(this, CommandNames.RemoveActivity, Activity);
            }
            await Navigation.PopModalAsync();
        });
    }
}
