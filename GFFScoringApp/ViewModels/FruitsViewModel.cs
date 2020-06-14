using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class FruitsViewModel : BaseViewModel
    {
        private Fruit _selectedFruit = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Fruit> Fruits { get; set; }

        public Fruit SelectedFruit
        {
            get => _selectedFruit;
            set
            {
                _selectedFruit = value;
                if (_selectedFruit != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.AddFruitSelection(Fruits.Where(fruit => fruit.IsSelected).ToList());
            await PushAsync(new BoostsPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public FruitsViewModel()
        {
            Title = "Select a Fruit";
            Fruits = new ObservableCollection<Fruit>()
            {
                new Fruit() {ImageUrl = ImageSource.FromFile("raspberry.png"), Name = "raspberry", HealthBonus = 15, Sweetness = 1},
                new Fruit() {ImageUrl = ImageSource.FromFile("blueberry.png"), Name = "blueberry", HealthBonus = 20, Sweetness = 2, ExtraHealthBonus = 20, IsSuperFood = true},
                new Fruit() {ImageUrl = ImageSource.FromFile("gojiberry.png"), Name = "gojiberries", HealthBonus = 20, Sweetness = 4, IsSuperFood = true},
                new Fruit() {ImageUrl = ImageSource.FromFile("peach.png"), Name = "peach", HealthBonus = 10, Sweetness = 3},
                new Fruit() {ImageUrl = ImageSource.FromFile("pineapple.png"), Name = "pineapple", HealthBonus = 10, Sweetness = 5},
                new Fruit() {ImageUrl = ImageSource.FromFile("strawberry.png"), Name = "strawberry", HealthBonus = 15, Sweetness = 2},
                new Fruit() {ImageUrl = ImageSource.FromFile("lemon.png"), Name = "lemon", HealthBonus = 10},
                new Fruit() {ImageUrl = ImageSource.FromFile("blackberry.png"), Name = "blackberry", HealthBonus = 15, Sweetness = 1},
                new Fruit() {ImageUrl = ImageSource.FromFile("apple.png"), Name = "apple", HealthBonus = 14},
                new Fruit() {ImageUrl = ImageSource.FromFile("banana.png"), Name = "banana", HealthBonus = 10, Sweetness = 5, CanBeSweetener =true},
                new Fruit() {ImageUrl = ImageSource.FromFile("grape.png"), Name = "grapes", HealthBonus = 10, CanBeSweetener = true},
                new Fruit() {ImageUrl = ImageSource.FromFile("mango.png"), Name = "mango", HealthBonus = 14},
                new Fruit() {ImageUrl = ImageSource.FromFile("watermelon.png"), Name = "watermelon", HealthBonus = 12, CanBeSweetener = true},
            };

            MessagingCenter.Subscribe<Fruit>(this, "toggledfruit", (sender) => IsNextEnabled = Fruits.Any(fruit => fruit.IsSelected));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}