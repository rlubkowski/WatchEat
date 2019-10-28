using WatchEat.ViewModels.Food;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        public FoodPage()
        {
            InitializeComponent();
            BindingContext = new FoodViewModel();
        }

        public FoodPage(FoodViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext as FoodViewModel;
            await viewModel.InitializeAsync(Navigation);
            if (viewModel.IsEditView)
            {
                var toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(FoodViewModel.Remove)));
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