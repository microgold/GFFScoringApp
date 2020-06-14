using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class BoostsViewModel : BaseViewModel
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
            summary.AddBoostSelection(Boosts.Where(boost => boost.IsSelected).ToList());
            await PushAsync(new SummaryPage());
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
                new Boost() {ImageUrl = ImageSource.FromFile("almondbutter.png"), Name = "almond butter", HealthBonus = 15, Fat = 3, Protein = 2},
                new Boost() {ImageUrl = ImageSource.FromFile("hemp.png"), Name = "hemp seeds", HealthBonus = 20, Protein = 4},
                new Boost() {ImageUrl = ImageSource.FromFile("mushroompowder.png"), Name = "mushroom powder", HealthBonus = 15, Protein = 3},
                new Boost() {ImageUrl = ImageSource.FromFile("cocoanibs.png"), Name = "raw cocoa nibs", HealthBonus = 15, Fat = 4},
                new Boost() {ImageUrl = ImageSource.FromFile("flaxseeds.png"), Name = "flax seeds", HealthBonus = 15, Fat = 1, Protein = 3},
                new Boost() {ImageUrl = ImageSource.FromFile("coconutoil.png"), Name = "cocunut oil", HealthBonus = 15, Fat = 5},
                new Boost() {ImageUrl = ImageSource.FromFile("peanutbutter.png"), Name = "peanut butter", HealthBonus = 10, Fat = 3, Protein = 3},
                new Boost() {ImageUrl = ImageSource.FromFile("yogurt.png"), Name = "whole yogurt", HealthBonus = 15, Fat = 3, Protein = 4},
                new Boost() {ImageUrl = ImageSource.FromFile("beepollen.png"), Name = "bee pollen", HealthBonus = 20, Sweetness = 1, Protein = 1},
                new Boost() {ImageUrl = ImageSource.FromFile("spirulina.png"), Name = "spirulina", HealthBonus = 20, Protein = 3},
                new Boost() {ImageUrl = ImageSource.FromFile("wheypowder.png"), Name = "whey powder", HealthBonus = 10, Sweetness = 4, Protein = 3},
            };

            MessagingCenter.Subscribe<Boost>(this, "toggledboost", (sender) => IsNextEnabled = Boosts.Any(boost => boost.IsSelected));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}