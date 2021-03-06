﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WatchEat.Controls;
using WatchEat.Helpers;
using WatchEat.Helpers.MethodExtensions;
using WatchEat.Models.Database;
using WatchEat.Resources;
using WatchEat.Views.Activity;
using Xamarin.Forms;

namespace WatchEat.ViewModels.Activity
{
    public class ActivitiesViewModel : ViewModelWithChildPages
    {
        public ActivitiesViewModel()
        {
            Activities = new ObservableCollection<ActivityEntry>();
            Title = AppResource.Activities;
        }

        public ObservableCollection<ActivityEntry> Activities { get; private set; }

        public ICommand OpenAddActivityPage => new AsyncCommand(async () =>
        {
            var page = new StyledNavigationPage(new ActivityPage());
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        public ICommand ActivitySelected => new AsyncCommand(async (param) =>
        {
            var activity = (ActivityEntry)param;
            var page = new StyledNavigationPage(new ActivityPage(new ActivityViewModel(activity)));
            HandleChildPageEvents(page);
            await Navigation.PushModalAsync(page);
        });

        protected override void OnChildPageDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.AddActivity);            
            MessagingCenter.Unsubscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.RemoveActivity);
            base.OnChildPageDisappearing(sender, e);
        }

        protected override void OnChildPageAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.AddActivity, (obj, item) =>
            {
                Activities.Add(item);
            });

            MessagingCenter.Subscribe<ActivityViewModel, ActivityEntry>(this, CommandNames.RemoveActivity, (obj, item) =>
            {
                Activities.Remove(item);                
            });
        }

        public async override Task InitializeAsync(INavigation navigation)
        {
            await base.InitializeAsync(navigation);
            Activities.Clear();
            Activities.AddRange(await DataStore.ActivityEntries.Get());
        }
    }
}
