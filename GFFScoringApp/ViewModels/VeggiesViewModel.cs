using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class VeggiesViewModel : BaseViewModel
    {
        private Veggie _selectedVeggie = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Veggie> Veggies { get; set; }

        public Veggie SelectedVeggie
        {
            get => _selectedVeggie;
            set
            {
                _selectedVeggie = value;
                if (_selectedVeggie != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        public ICommand SelectedVeggieCommand { get; set; }

        private void OnSelectedAVeggie(object val)
        {
            // if any veggie is selected, then enable the button
            IsNextEnabled = Veggies.Any(veggie => veggie.IsSelected);
        }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.AddVeggieSelection(Veggies.Where(veggie => veggie.IsSelected).ToList());

            await PushAsync(new FruitsPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public VeggiesViewModel()
        {
            MessagingCenter.Subscribe<Veggie>(this, "toggledveggie", (sender) => IsNextEnabled = Veggies.Any(veggie => veggie.IsSelected));

            Title = "Select a Veggie";
            Veggies = new ObservableCollection<Veggie>()
            {
                new Veggie() {ImageUrl = ImageSource.FromFile("avocado.png"), Name = "avocado", HealthBonus = 20, Fat = 3, Protein = 1, IsSuperFood = true, CanBeBoost = true},
                new Veggie() {ImageUrl = ImageSource.FromFile("carrot.png"), Name = "carrot", HealthBonus=15, Sweetness = 2},
                new Veggie() {ImageUrl = ImageSource.FromFile("beet.png"), Name = "beet", HealthBonus=15, Sweetness = 2, CanBeSweetener = true},
                new Veggie() {ImageUrl = ImageSource.FromFile("spinach.png"), Name = "spinach", HealthBonus = 15, Protein = 1},
                new Veggie() {ImageUrl = ImageSource.FromFile("parsley.png"), Name = "parsley", HealthBonus = 15},
                new Veggie() {ImageUrl = ImageSource.FromFile("kale.png"), Name = "kale", HealthBonus = 20, Protein = 1, IsSuperFood = true},
                new Veggie() {ImageUrl = ImageSource.FromFile("fennel.png"), Name = "fennel", HealthBonus = 15},
                new Veggie() {ImageUrl = ImageSource.FromFile("ginger.png"), Name = "ginger", HealthBonus = 15},
                new Veggie() {ImageUrl = ImageSource.FromFile("redcabbage.png"), Name = "red cabbage", HealthBonus = 15, Sweetness = 1},
                new Veggie() {ImageUrl = ImageSource.FromFile("turmeric.png"), Name = "turmeric", HealthBonus = 20, IsSuperFood = true, CanBeBoost = true},
                new Veggie() {ImageUrl = ImageSource.FromFile("mint.png"), Name = "mint", HealthBonus = 10},
                new Veggie() {ImageUrl = ImageSource.FromFile("cucumber.png"), Name = "cucumber", HealthBonus = 10},

            };


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}