using WatchEat.Resources;
using WatchEat.ViewModels.Activity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        public ActivityPage()
        {
            InitializeComponent();
            BindingContext = new ActivityViewModel();
        }

        public ActivityPage(ActivityViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext as ActivityViewModel;
            await viewModel.InitializeAsync(Navigation);
            if (viewModel.IsEditView)
            {
                var toolbarItem = new ToolbarItem();
                toolbarItem.Text = AppResource.Remove;
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(ActivityViewModel.Remove)));
                ActivityPageRef.ToolbarItems.Add(toolbarItem);
            }
            else
            {
                BindingContext = viewModel;
            }
            base.OnAppearing();
        }
    }
}