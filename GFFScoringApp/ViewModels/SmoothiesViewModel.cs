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
        private Item _selectedSmoothie = null;
        private bool _isNextEnabled = false;
        public ObservableCollection<Item> Smoothies { get; set; }

        public Item SelectedSmoothie
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
            await PushAsync(new SmoothiePage());
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { _isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled));}
        }

        public SmoothiesViewModel()
        {
            Title = "Characters";
            Smoothies = new ObservableCollection<Item>()
            {
                new Item() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Crappy Frappe"},
                new Item() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Veggie Wedgie"},
                new Item() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Quickie Lickie"},
                new Item() {ImageUrl = ImageSource.FromFile("crappyfrappe.jpg"), Name = "Fruit Punch"},
            };

            SelectSmoothieCommand = new Command(OnSelectedSmoothie);
        }

      
    }

}