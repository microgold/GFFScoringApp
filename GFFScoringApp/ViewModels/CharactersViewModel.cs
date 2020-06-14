using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class CharactersViewModel : BaseViewModel
    {
        private Character _selectedCharacter = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Character> Characters { get; set; }

        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set { _selectedCharacter = value;
                if (_selectedCharacter != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectCharacterCommand { get; set; }

        private async void OnSelectedCharacter()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.SelectedCharacter = _selectedCharacter;

            await PushAsync(new SmoothiePage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public CharactersViewModel()
        {
            Title = "Select a Character";
            Characters = new ObservableCollection<Character>()
            {
                new Character() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name = "Good Food Fighter"},
                new Character() {ImageUrl = ImageSource.FromFile("ketoken.png"), Name = "Keto Ken"},
                new Character() {ImageUrl = ImageSource.FromFile("healthyheather.png"), Name = "Healthy Heather"},
                new Character() {ImageUrl = ImageSource.FromFile("paleopete.png"), Name = "Paleo Pete"},
            };

            SelectCharacterCommand = new Command(OnSelectedCharacter);
        }

      
    }

}