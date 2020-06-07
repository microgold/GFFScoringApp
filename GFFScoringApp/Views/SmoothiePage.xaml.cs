using System;
using System.ComponentModel;
using Xamarin.Forms;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;

namespace GFFScoringApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SmoothiePage : ContentPage
    {
        SmoothiesViewModel viewModel;

        public SmoothiePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SmoothiesViewModel();
            viewModel.Navigation = Navigation;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Smoothies.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}