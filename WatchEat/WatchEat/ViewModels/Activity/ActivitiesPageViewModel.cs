using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Helpers;
using WatchEat.Views.Activity;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivitiesPageViewModel : BaseViewModel
    {
        public ActivitiesPageViewModel()
        {
            Activities = new ObservableCollection<Models.Database.ActivityEntry>();
        }

        public ObservableCollection<Models.Database.ActivityEntry> Activities { get; private set; }

        public ICommand OpenAddActivityPage => new AsyncCommand(async () =>
        {
            await Navigation.PushModalAsync(new NavigationPage(new ActivityPage()));
        });

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (Models.Database.ActivityEntry)param;
            await Navigation.PushModalAsync(new NavigationPage(new ActivityPage(new ActivityPageViewModel(activity))));
        });

        public async override Task InitializeAsync(INavigation navigation)
        {
            if (!IsInitialized)
            {
                foreach (var activity in await DataStore.ActivityEntries.Get())
                {
                    Activities.Add(activity);
                }

                MessagingCenter.Subscribe<ActivityPageViewModel, Models.Database.ActivityEntry>(this, CommandNames.AddActivity, async (obj, item) =>
                {
                    Activities.Add(item);
                    await DataStore.ActivityEntries.Insert(item);
                });

                MessagingCenter.Subscribe<ActivityPageViewModel, Models.Database.ActivityEntry>(this, CommandNames.EditActivity, async (obj, item) =>
                {
                    await DataStore.ActivityEntries.Update(item);
                });

                MessagingCenter.Subscribe<ActivityPageViewModel, Models.Database.ActivityEntry>(this, CommandNames.RemoveActivity, async (obj, item) =>
                {
                    Activities.Remove(item);
                    await DataStore.ActivityEntries.Delete(item);
                });

                await base.InitializeAsync(navigation);
            }
        }
    }
}
