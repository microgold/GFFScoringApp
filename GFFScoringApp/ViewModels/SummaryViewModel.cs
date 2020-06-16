using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Interfaces;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    internal class SummaryViewModel : BaseViewModel
    {
        private bool _isNextEnabled = false;
        public ObservableCollection<Ingredient> SelectedVeggies { get; set; }
        public ObservableCollection<Ingredient> SelectedFruits { get; set; }
        public ObservableCollection<Ingredient> SelectedSweeteners { get; set; }
        public ObservableCollection<Ingredient> SelectedBoosts { get; set; }

        public ISummary Summary { get; private set; }
        public ICommand SelectCharacterCommand { get; set; }

        private async void OnSelectedCharacter()
        {
            await PushAsync(new SmoothiePage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public SummaryViewModel()
        {
            Title = "Summary of Your Score";
            Summary = DependencyService.Resolve<ISummary>();

            SelectedVeggies = new ObservableCollection<Ingredient>(Summary.SelectedVeggies);
            SelectedFruits = new ObservableCollection<Ingredient>(Summary.SelectedFruits);
            SelectedBoosts = new ObservableCollection<Ingredient>(Summary.SelectedBoosts);
            SelectedSweeteners = new ObservableCollection<Ingredient>(Summary.SelectedSweeteners);


            SelectCharacterCommand = new Command(OnSelectedCharacter);
        }

      
    }

}