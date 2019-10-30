using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Models.Database;
using WatchEat.Views.Activity;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivitiesViewModel : MessagesSubscribedViewModel
    {
        public ActivitiesViewModel()
        {
            Activities = new ObservableCollection<ActivityEntry>();
        }

        public ObservableCollection<ActivityEntry> Activities { get; private set; }

        public ICommand OpenAddActivityPage => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new ActivityPage());
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (ActivityEntry)param;
            var page = new StyledNavigationPage(new ActivityPage(new ActivityViewModel(activity)));
            HandlePageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.AddActivity);
            MessagingCenter.Unsubscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.EditActivity);
            MessagingCenter.Unsubscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.RemoveActivity);
            base.OnPageDisappearing(sender, e);
        }

        protected override void OnPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.AddActivity, async (obj, item) =>
            {
                Activities.Add(item);
                await DataStore.ActivityEntries.Insert(item);
            });

            MessagingCenter.Subscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.EditActivity, async (obj, item) =>
            {
                await DataStore.ActivityEntries.Update(item);
            });

            MessagingCenter.Subscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.RemoveActivity, async (obj, item) =>
            {
                Activities.Remove(item);
                await DataStore.ActivityEntries.Delete(item);
            });
        }

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);

            foreach (var activity in await DataStore.ActivityEntries.Get())
            {
                Activities.Add(activity);
            }
        }
    }
}
