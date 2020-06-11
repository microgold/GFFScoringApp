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
                new Smoothie() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Veggie Wedgie"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Quickie Lickie"},
                new Smoothie() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Fruit Punch"},
            };

            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

      
    }

}