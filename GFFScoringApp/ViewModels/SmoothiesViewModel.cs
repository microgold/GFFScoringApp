using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.ViewModels;
using Xamarin.Forms;

namespace GFFScoringApp.Views
{
    internal class SmoothiesViewModel : BaseViewModel
    {
        private Smoothie _selectedSmoothie = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Smoothie> Smoothies { get; set; }

        public Smoothie SelectedSmoothie
        {
            get => _selectedSmoothie;
            set { _selectedSmoothie = value;
                if (_selectedSmoothie != null)
                {
                    IsNextEnabled = true;
                }
            }
        }

        public ICommand SelectSmoothieCommand { get; set; }

        private async void OnSelectedSmoothie()
        {
            var summary = DependencyService.Resolve<ISummary>();
            summary.SelectedSmoothie = _selectedSmoothie;

            await PushAsync(new VeggiesPage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public SmoothiesViewModel()
        {
            Title = "Select a Smoothie";
            Smoothies = new ObservableCollection<Smoothie>()
            {
                new Smoothie() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Crappy Frappe"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("veggiewedgie.jpg"), Name = "Veggie Wedgie"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("quickylicky.jpg"), Name = "Quickie Lickie"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("fruitpunch.png"), Name = "Fruit Punch"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("rainbowglow.png"), Name = "Rainbow Glow"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("neatoketo.png"), Name = "Neato Keto"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("superfoodsallie.png"), Name = "Superfood Sallie"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("berryboostblitz.png"), Name = "Berryboost Blitz"},
            };

            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

      
    }

}