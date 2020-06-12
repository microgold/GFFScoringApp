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
            await PushAsync(new SummaryPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public FruitsViewModel()
        {
            Title = "Select a Smoothie";
            Fruits = new ObservableCollection<Fruit>()
            {
                new Fruit() {ImageUrl = ImageSource.FromFile("apple.png"), Name = "apple", HealthBonus = 14},
                new Fruit() {ImageUrl = ImageSource.FromFile("banana.png"), Name = "banana", HealthBonus = 10},
                new Fruit() {ImageUrl = ImageSource.FromFile("grape.png"), Name = "grapes", HealthBonus = 10},
                new Fruit() {ImageUrl = ImageSource.FromFile("mango.png"), Name = "mango", HealthBonus = 14},
                new Fruit() {ImageUrl = ImageSource.FromFile("date.png"), Name = "date", HealthBonus = 12},
                new Fruit() {ImageUrl = ImageSource.FromFile("watermelon.png"), Name = "watermelon", HealthBonus = 12},
            };

            MessagingCenter.Subscribe<Fruit>(this, "toggledfruit", (sender) => IsNextEnabled = Fruits.Any(fruit => fruit.IsSelected));


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}