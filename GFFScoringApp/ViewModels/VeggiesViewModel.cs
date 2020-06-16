using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Interfaces;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    internal class VeggiesViewModel : BaseViewModel, ICheckIngredientRequirements<Veggie>
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

            summary.ClearVeggieSelection();
            summary.AddVeggieSelection(Veggies.Where(veggie => veggie.IsSelected && !veggie.UseAsBoost && !veggie.UseAsSweetener).Cast<Ingredient>().ToList());
            summary.AddBoostSelection(Veggies.Where(veggie => veggie.IsSelected && veggie.UseAsBoost).Cast<Ingredient>().ToList());
            summary.AddSweetenerSelection(Veggies.Where(veggie => veggie.IsSelected && veggie.UseAsSweetener).Cast<Ingredient>().ToList());

            await PushAsync(new FruitsPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public VeggiesViewModel()
        {
            MessagingCenter.Subscribe<Veggie>(this, "toggledveggie", (sender) => IsNextEnabled = DoesIngredientsMeetSmoothieRequirements(sender));

            Title = "Select a Veggie";
            Veggies = new ObservableCollection<Veggie>()
            {
                new Veggie() {ImageUrl = ImageSource.FromFile("avocado.png"), Name = "avocado", HealthBonus = 20, Fat = 3, Protein = 1, IsSuperFood = true, CanBeBoost = true, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("carrot.png"), Name = "carrot", HealthBonus=15, Sweetness = 2, Color = Enums.Color.Orange},
                new Veggie() {ImageUrl = ImageSource.FromFile("beet.png"), Name = "beet", HealthBonus=15, Sweetness = 2, CanBeSweetener = true, Color = Enums.Color.Red},
                new Veggie() {ImageUrl = ImageSource.FromFile("spinach.png"), Name = "spinach", HealthBonus = 15, Protein = 1, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("parsley.png"), Name = "parsley", HealthBonus = 15, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("kale.png"), Name = "kale", HealthBonus = 20, Protein = 1, IsSuperFood = true, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("fennel.png"), Name = "fennel", HealthBonus = 15, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("ginger.png"), Name = "ginger", HealthBonus = 15, Color = Enums.Color.Brown},
                new Veggie() {ImageUrl = ImageSource.FromFile("redcabbage.png"), Name = "red cabbage", HealthBonus = 15, Sweetness = 1, Color = Enums.Color.Purple},
                new Veggie() {ImageUrl = ImageSource.FromFile("turmeric.png"), Name = "turmeric", HealthBonus = 20, IsSuperFood = true, CanBeBoost = true, Color = Enums.Color.Orange},
                new Veggie() {ImageUrl = ImageSource.FromFile("mint.png"), Name = "mint", HealthBonus = 10, Color = Enums.Color.Green},
                new Veggie() {ImageUrl = ImageSource.FromFile("cucumber.png"), Name = "cucumber", HealthBonus = 10, Color = Enums.Color.Green},

            };


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


        public bool DoesIngredientsMeetSmoothieRequirements(Veggie ingredient)
        {
            var summary = DependencyService.Resolve<ISummary>();
            var smoothie = summary.SelectedSmoothie;
            var numberOfSelectedVeggies = Veggies.Count(veggie => veggie.IsSelected && !veggie.UseAsSweetener && !veggie.UseAsBoost);
            return smoothie.VeggieRequirement == numberOfSelectedVeggies;
        }
    }

}