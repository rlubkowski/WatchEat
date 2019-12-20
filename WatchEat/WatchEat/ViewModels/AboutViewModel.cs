using System;
using System.Windows.Input;
using WatchEat.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WatchEat.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = AppResource.About;
        }

        public ICommand ContactMe => new Command(() => Launcher.OpenAsync(new Uri($"mailto:roman@lubkowski.net")));
    }
}