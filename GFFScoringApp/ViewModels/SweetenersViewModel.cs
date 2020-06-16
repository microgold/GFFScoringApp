using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Interfaces;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GFFScoringApp.ViewModels
{
    internal class SweetenersViewModel : BaseViewModel, ICheckIngredientRequirements<Sweetener>
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
            summary.ClearSweetenerSelection();
            summary.AddSweetenerSelection(Sweeteners.Where(sweetener => sweetener.IsSelected).Cast<Ingredient>().ToList());
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
                new Sweetener() {ImageUrl = ImageSource.FromFile("honey.png"), Name = "honey", HealthBonus = 5, Sweetness = 6, Color = Enums.Color.Brown},
                new Sweetener() {ImageUrl = ImageSource.FromFile("date.png"), Name = "date", HealthBonus = 10, Sweetness = 6, Color = Enums.Color.Brown},
                new Sweetener() {ImageUrl = ImageSource.FromFile("cinnamon.png"), Name = "cinnamon", HealthBonus = 10, Sweetness = 2, Color = Enums.Color.Brown},
            };


            HandleSweetenerSubstitutes();


            MessagingCenter.Subscribe<Sweetener>(this, "toggledsweetener", (sender) => IsNextEnabled = DoesIngredientsMeetSmoothieRequirements(sender));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

        protected void HandleSweetenerSubstitutes()
        {
            var summary = DependencyService.Resolve<ISummary>();

            var sweetenerSubstitutes =
                summary.SelectedSweeteners.Where(ingredient => ingredient.UseAsSweetener).ToList();

            sweetenerSubstitutes.ForEach(sweetener => Sweeteners.Add(ToSweetener(sweetener)));
            if (sweetenerSubstitutes.Count > 0)
            {
                IsNextEnabled = DoesIngredientsMeetSmoothieRequirements(Sweeteners.LastOrDefault());
            }
        }

        private Sweetener ToSweetener(Ingredient sweetener)
        {
            return new Sweetener()
            {
                ImageUrl = sweetener.ImageUrl, Name = sweetener.Name,
                HealthBonus = sweetener.HealthBonus, 
                Sweetness = sweetener.Sweetness,
                Protein = sweetener.Protein,
                Fat = sweetener.Fat,
                Color = sweetener.Color,
                IsSelected = sweetener.IsSelected
            };

        }

        public bool DoesIngredientsMeetSmoothieRequirements(Sweetener ingredient)
        {
                var summary = DependencyService.Resolve<ISummary>();
                var smoothie = summary.SelectedSmoothie;
                var numberOfSelectedSweeteners = Sweeteners.Count(fruit => fruit.IsSelected);

                return smoothie.SweetenerRequirement == numberOfSelectedSweeteners;
        }
    }

}