using WatchEat.ViewModels.TrainingActivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.TrainingActivity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleActivityPage : ContentPage
    {
        public SingleActivityPage()
        {
            InitializeComponent();
        }

        public SingleActivityPage(SingleTrainingActivityPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext != null ? BindingContext as SingleTrainingActivityPageViewModel : new SingleTrainingActivityPageViewModel();
            await viewModel.InitializeAsync(Navigation, DisplayAlert);
            if (viewModel.IsEditView)
            {
                var toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(SingleTrainingActivityPageViewModel.Remove)));
                PageRef.ToolbarItems.Add(toolbarItem);
            }
            else
            {
                BindingContext = viewModel;
            }
            base.OnAppearing();
        }
    }
}