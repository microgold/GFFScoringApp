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
    public partial class SweetenersPage : ContentPage
    {
        SweetenersViewModel viewModel;

        public SweetenersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SweetenersViewModel();
            viewModel.Navigation = Navigation;
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Sweeteners.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}