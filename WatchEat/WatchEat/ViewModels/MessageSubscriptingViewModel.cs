using System;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public abstract class MessageSubscriptingViewModel : BaseViewModel
    {
        protected void HandlePageEvents(NavigationPage page)
        {
            page.Appearing += OnPageAppearing;
            page.Disappearing += OnPageDisappearing;
        }

        protected virtual void OnPageAppearing(object sender, EventArgs e)
        {

        }

        protected virtual void OnPageDisappearing(object sender, EventArgs e)
        {
            var page = (sender as NavigationPage);
            page.Appearing -= OnPageAppearing;
            page.Disappearing -= OnPageDisappearing;
        }
    }
}
