using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class VeggiesViewModel : BaseViewModel
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
            summary.AddVeggieSelection(Veggies.Where(veggie => veggie.IsSelected).ToList());

            await PushAsync(new FruitsPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }

        public VeggiesViewModel()
        {
            MessagingCenter.Subscribe<Veggie>(this, "toggledveggie", (sender) => IsNextEnabled = Veggies.Any(veggie => veggie.IsSelected));

            Title = "Select a Fruit";
            Veggies = new ObservableCollection<Veggie>()
            {
                new Veggie() {ImageUrl = ImageSource.FromFile("avocado.png"), Name = "avocado", HealthBonus = 16},
                new Veggie() {ImageUrl = ImageSource.FromFile("carrot.png"), Name = "carrot", HealthBonus=12},
                new Veggie() {ImageUrl = ImageSource.FromFile("beet.png"), Name = "beet", HealthBonus=14},
                new Veggie() {ImageUrl = ImageSource.FromFile("spinach.png"), Name = "spinach", HealthBonus = 14},
                new Veggie() {ImageUrl = ImageSource.FromFile("parsley.png"), Name = "parsley", HealthBonus = 18},
                new Veggie() {ImageUrl = ImageSource.FromFile("kale.png"), Name = "kale", HealthBonus = 18},

            };


            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }


    }

}