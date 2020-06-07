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
        private Item _selectedCharacter = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Item> Characters { get; set; }

        public Item SelectedCharacter
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
            await PushAsync(new SmoothiePage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public CharactersViewModel()
        {
            Title = "Characters";
            Characters = new ObservableCollection<Item>()
            {
                new Item() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name = "Good Food Fighter"},
                new Item() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name = "Keto Ken"},
                new Item() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name = "Healthy Heather"},
                new Item() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name = "Paleo Pete"},
            };

            SelectCharacterCommand = new Command(OnSelectedCharacter);
        }

      
    }

}