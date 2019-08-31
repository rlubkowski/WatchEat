﻿using WatchEat.ViewModels.EventSelection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchEat.Views.EventSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaterActivityPage : ContentPage
    {
        public WaterActivityPage(WaterActivityPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext as WaterActivityPageViewModel;
            await viewModel.InitializeAsync(Navigation, DisplayAlert);
            if (viewModel.IsEditView)
            {
                var toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Remove";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding(nameof(WaterActivityPageViewModel.Remove)));
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