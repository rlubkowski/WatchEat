using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using WatchEat.Services;
using WatchEat.Services.Interfaces;
using Xamarin.Forms;

namespace WatchEat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterServices();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=d052f377-ac14-4f09-b175-fea95222709f;android=9d5b0155-9633-438c-b1b1-6beadbd63faf;",typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterServices()
        {
            DependencyService.Register<IDataStore, DataStore>();            
        }
    }
}
