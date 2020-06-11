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
    public partial class VeggiesPage : ContentPage
    {
        VeggiesViewModel viewModel;

        public VeggiesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VeggiesViewModel();
            viewModel.Navigation = Navigation;
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Veggies.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}