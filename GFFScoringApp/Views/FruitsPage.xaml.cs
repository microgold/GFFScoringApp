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
    public partial class FruitsPage : ContentPage
    {
        FruitsViewModel viewModel;

        public FruitsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FruitsViewModel();
            viewModel.Navigation = Navigation;
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Fruits.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}