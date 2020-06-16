using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Interfaces;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class BoostsViewModel : BaseViewModel, ICheckIngredientRequirements<Boost>
    {
        private Boost _selectedBoost = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Boost> Boosts { get; set; }

        public Boost SelectedBoost
        {
            get => _selectedBoost;
            set
            {
                _selectedBoost = value;
                if (_selectedBoost != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.ClearBoostSelection();
            summary.AddBoostSelection(Boosts.Where(boost => boost.IsSelected).Cast<Ingredient>().ToList());
            await PushAsync(new SweetenersPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public BoostsViewModel()
        {
            Title = "Select a Boost";
            Boosts = new ObservableCollection<Boost>()
            {
                new Boost() {ImageUrl = ImageSource.FromFile("almondbutter.png"), Name = "almond butter", HealthBonus = 15, Fat = 3, Protein = 2, Color = Enums.Color.Brown},
                new Boost() {ImageUrl = ImageSource.FromFile("hemp.png"), Name = "hemp seeds", HealthBonus = 20, Protein = 4, IsSuperFood = true, Color = Enums.Color.Brown},
                new Boost() {ImageUrl = ImageSource.FromFile("mushroompowder.png"), Name = "mushroom powder", HealthBonus = 15, Protein = 3, Color = Enums.Color.White},
                new Boost() {ImageUrl = ImageSource.FromFile("cocoanibs.png"), Name = "raw cocoa nibs", HealthBonus = 15, Fat = 4, Color = Enums.Color.Brown},
                new Boost() {ImageUrl = ImageSource.FromFile("flaxseeds.png"), Name = "flax seeds", HealthBonus = 15, Fat = 1, Protein = 3, Color = Enums.Color.Brown},
                new Boost() {ImageUrl = ImageSource.FromFile("coconutoil.png"), Name = "cocunut oil", HealthBonus = 15, Fat = 5, Color = Enums.Color.White},
                new Boost() {ImageUrl = ImageSource.FromFile("peanutbutter.png"), Name = "peanut butter", HealthBonus = 10, Fat = 3, Protein = 3, Color = Enums.Color.Brown},
                new Boost() {ImageUrl = ImageSource.FromFile("yogurt.png"), Name = "whole yogurt", HealthBonus = 15, Fat = 3, Protein = 4, Color = Enums.Color.White},
                new Boost() {ImageUrl = ImageSource.FromFile("beepollen.png"), Name = "bee pollen", HealthBonus = 20, Sweetness = 1, Protein = 1, IsSuperFood = true, Color = Enums.Color.Yellow},
                new Boost() {ImageUrl = ImageSource.FromFile("spirulina.png"), Name = "spirulina", HealthBonus = 20, Protein = 3, IsSuperFood = true, Color = Enums.Color.Green},
                new Boost() {ImageUrl = ImageSource.FromFile("wheypowder.png"), Name = "whey powder", HealthBonus = 10, Sweetness = 4, Protein = 3, Color = Enums.Color.White},
            };

            HandleBoostSubstitutes();

            MessagingCenter.Subscribe<Boost>(this, "toggledboost", (sender) => IsNextEnabled = IsNextEnabled = DoesIngredientsMeetSmoothieRequirements(sender));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

        protected void HandleBoostSubstitutes()
        {
            var summary = DependencyService.Resolve<ISummary>();

            var boostSubstitutes =
                summary.SelectedBoosts.Where(ingredient => ingredient.UseAsBoost).ToList();

            boostSubstitutes.ForEach(boost => Boosts.Add(ToBoost(boost)));
            if (boostSubstitutes.Count > 0)
            {
                IsNextEnabled = DoesIngredientsMeetSmoothieRequirements(Boosts.LastOrDefault());
            }
        }

        private Boost ToBoost(Ingredient ingredient)
        {
            return new Boost()
            {
                ImageUrl = ingredient.ImageUrl,
                Name = ingredient.Name,
                HealthBonus = ingredient.HealthBonus,
                Sweetness = ingredient.Sweetness,
                Protein = ingredient.Protein,
                Fat = ingredient.Fat,
                Color = ingredient.Color,
                IsSelected = ingredient.IsSelected
            };

        }


        public bool DoesIngredientsMeetSmoothieRequirements(Boost ingredient)
        {
            var summary = DependencyService.Resolve<ISummary>();
            var smoothie = summary.SelectedSmoothie;
            var numberOfSelectedBoosts = Boosts.Count(fruit => fruit.IsSelected);

            return smoothie.BoostRequirement == numberOfSelectedBoosts;
        }
    }

}