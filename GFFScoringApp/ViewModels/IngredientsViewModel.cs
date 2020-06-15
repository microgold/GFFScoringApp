using System.Collections.ObjectModel;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    internal class IngredientsViewModel : BaseViewModel
    {
        private Item _selectedIngredient = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Item> Ingredients { get; set; }

        public Item SelectedIngredient
        {
            get => _selectedIngredient;
            set { _selectedIngredient = value;
                if (_selectedIngredient != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectIngredientCommand { get; set; }

        private async void OnSelectedIngredient()
        {
            await PushAsync(new SmoothiePage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public IngredientsViewModel()
        {
            Title = "Select an Ingredient";
            Ingredients = new ObservableCollection<Item>()
            {
                new Ingredient() {ImageUrl = ImageSource.FromFile("carrot.png"), Name = "Carrot"},
                new Ingredient() {ImageUrl = ImageSource.FromFile("avocado.png"), Name = "Avocado"},
                new Ingredient() {ImageUrl = ImageSource.FromFile("apple.png"), Name = "Apple"},
                new Ingredient() {ImageUrl = ImageSource.FromFile("hemp.png"), Name = "Hemp"},
            };

            SelectIngredientCommand = new Command(OnSelectedIngredient);
        }

      
    }

}