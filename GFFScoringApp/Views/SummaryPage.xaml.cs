using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GFFScoringApp.Models;
using GFFScoringApp.Views;
using GFFScoringApp.ViewModels;

namespace GFFScoringApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SummaryPage : ContentPage
    {
        SummaryViewModel viewModel;

        public SummaryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SummaryViewModel();
            viewModel.Navigation = Navigation;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.SelectedFruits.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}