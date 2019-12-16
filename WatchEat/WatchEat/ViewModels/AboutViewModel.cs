using System;
using System.Windows.Input;
using WatchEat.Resources;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = AppResource.About;

            ContactMe = new Command(() => Device.OpenUri(new Uri($"mailto:roman@lubkowski.net")));
        }

        public ICommand ContactMe { get; }
    }
}