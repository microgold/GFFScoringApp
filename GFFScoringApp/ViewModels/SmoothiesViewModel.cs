using System.Collections.ObjectModel;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    internal class SmoothiesViewModel : BaseViewModel
    {
        private Smoothie _selectedSmoothie = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Smoothie> Smoothies { get; set; }

        public Smoothie SelectedSmoothie
        {
            get => _selectedSmoothie;
            set { _selectedSmoothie = value;
                if (_selectedSmoothie != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.SelectedSmoothie = _selectedSmoothie;

            await PushAsync(new VeggiesPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public SmoothiesViewModel()
        {
            Title = "Select a Smoothie";
            Smoothies = new ObservableCollection<Smoothie>()
            {
                new Smoothie()
                {
                    ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Crappy Frappe",
                    SweetenerRequirement = 1, FruitRequirement = 3, VeggieRequirement = 0, BoostRequirement = 2,
                    MinimumSweetnessRequirement = 8, MinimumFatRequirement = 4, MinimumProteinRequirement = 4
                },
                new Smoothie()
                {
                    ImageUrl = ImageSource.FromFile("veggiewedgie.jpg"), Name = "Veggie Wedgie",
                    SweetenerRequirement = 1, FruitRequirement = 1, VeggieRequirement = 4, BoostRequirement = 2,
                    MinimumSweetnessRequirement = 5, MinimumFatRequirement = 3, MinimumProteinRequirement = 3
                },
                new Smoothie()
                {
                    ImageUrl = ImageSource.FromFile("quickylicky.jpg"), Name = "Quickie Lickie",
                    SweetenerRequirement = 1, FruitRequirement = 1, VeggieRequirement = 1, BoostRequirement = 1,
                    MinimumSweetnessRequirement = 6, MinimumFatRequirement = 1, MinimumProteinRequirement = 3
                },
                new Smoothie()
                {
                    ImageUrl = ImageSource.FromFile("fruitpunch.png"), Name = "Fruit Punch",
                    SweetenerRequirement = 1, FruitRequirement = 3, VeggieRequirement = 1, BoostRequirement = 1,
                    MinimumSweetnessRequirement = 6, MinimumFatRequirement = 3, MinimumProteinRequirement = 3
                },
                new Smoothie() {ImageUrl = ImageSource.FromFile("rainbowglow.png"), Name = "Rainbow Glow",
                    SweetenerRequirement = 1, FruitRequirement = 2, VeggieRequirement = 2, BoostRequirement = 1,
                    MinimumSweetnessRequirement = 6, MinimumFatRequirement = 1, MinimumProteinRequirement = 3},
                new Smoothie()
                {
                    ImageUrl = ImageSource.FromFile("neatoketo.png"), Name = "Neato Keto",
                    SweetenerRequirement = 0, FruitRequirement = 1, VeggieRequirement = 2, BoostRequirement = 3,
                    MinimumSweetnessRequirement = 2, MinimumFatRequirement = 4, MinimumProteinRequirement = 4
                },
                new Smoothie() 
                    {
                        ImageUrl = ImageSource.FromFile("superfoodsallie.png"), Name = "Superfood Sallie",
                        SweetenerRequirement = 0, FruitRequirement = 2, VeggieRequirement = 1, BoostRequirement = 2,
                        MinimumSweetnessRequirement = 4, MinimumFatRequirement = 1, MinimumProteinRequirement = 4
                    },
                new Smoothie() 
                {
                    ImageUrl = ImageSource.FromFile("berryboostblitz.png"), Name = "Berryboost Blitz", 
                    SweetenerRequirement = 0, FruitRequirement = 2, VeggieRequirement = 2, BoostRequirement = 2, 
                    MinimumSweetnessRequirement = 5, MinimumFatRequirement = 4, MinimumProteinRequirement = 4, MinimumSuperfoodRequirement = 2
                },
            };

            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

      
    }

}