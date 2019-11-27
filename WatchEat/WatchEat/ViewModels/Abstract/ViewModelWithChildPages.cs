using System;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public abstract class ViewModelWithChildPages : BaseViewModel
    {
        protected void HandleChildPageEvents(NavigationPage page)
        {
            page.Appearing += OnChildPageAppearing;
            page.Disappearing += OnChildPageDisappearing;
        }

        protected virtual void OnChildPageAppearing(object sender, EventArgs e)
        {

        }

        protected virtual void OnChildPageDisappearing(object sender, EventArgs e)
        {
            var page = (sender as NavigationPage);
            page.Appearing -= OnChildPageAppearing;
            page.Disappearing -= OnChildPageDisappearing;
        }
    }
}
