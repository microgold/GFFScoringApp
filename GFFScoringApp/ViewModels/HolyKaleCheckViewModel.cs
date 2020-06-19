using GFFScoringApp.Interfaces;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    public class HolyKaleCheckViewModel : BaseViewModel
    {
        private bool _no;

        public HolyKaleCheckViewModel()
        {
            Title = "Mix-up Survey";
            SelectNextCommand = new Command(OnSelectedSmoothie);
        }

        public Command SelectNextCommand { get; set; }

        public bool HasCard { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.HasHolyKaleMixup = HasCard;
            await PushAsync(new SummaryPage());
        }



    }
}