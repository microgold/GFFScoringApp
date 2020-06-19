using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GFFScoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HolyKaleCheckPage : ContentPage
    {
        HolyKaleCheckViewModel viewModel;

        public HolyKaleCheckPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HolyKaleCheckViewModel();
            viewModel.Navigation = Navigation;
        }
    }
}