using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class SweetenersViewModel : BaseViewModel
    {
        private Sweetener _selectedSweetener = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Sweetener> Sweeteners { get; set; }

        public Sweetener SelectedSweetener
        {
            get => _selectedSweetener;
            set
            {
                _selectedSweetener = value;
                if (_selectedSweetener != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.AddSweetenerSelection(Sweeteners.Where(sweetener => sweetener.IsSelected).ToList());
            await PushAsync(new SummaryPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public SweetenersViewModel()
        {
            Title = "Select a Sweetener";
            Sweeteners = new ObservableCollection<Sweetener>()
            {
                new Sweetener() {ImageUrl = ImageSource.FromFile("honey.png"), Name = "honey", HealthBonus = 5, Sweetness = 6},
                new Sweetener() {ImageUrl = ImageSource.FromFile("date.png"), Name = "date", HealthBonus = 10, Sweetness = 6},
                new Sweetener() {ImageUrl = ImageSource.FromFile("cinnamon.png"), Name = "cinnamon", HealthBonus = 10, Sweetness = 2},
            };

            MessagingCenter.Subscribe<Sweetener>(this, "toggledsweetener", (sender) => IsNextEnabled = Sweeteners.Any(sweetener => sweetener.IsSelected));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}