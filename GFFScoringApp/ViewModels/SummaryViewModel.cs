﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using GFFScoringApp.Models;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.ViewModels
{
    internal class SummaryViewModel : BaseViewModel
    {
        private bool _isNextEnabled = false;
        public ObservableCollection<Veggie> SelectedVeggies { get; set; }
        public ObservableCollection<Fruit> SelectedFruits { get; set; }
        public ObservableCollection<Sweetener> SelectedSweeteners { get; set; }
        public ObservableCollection<Boost> SelectedBoosts { get; set; }

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

            SelectedVeggies = new ObservableCollection<Veggie>(Summary.SelectedVeggies);
            SelectedFruits = new ObservableCollection<Fruit>(Summary.SelectedFruits);
            SelectedBoosts = new ObservableCollection<Boost>(Summary.SelectedBoosts);
            SelectedSweeteners = new ObservableCollection<Sweetener>(Summary.SelectedSweeteners);


            SelectCharacterCommand = new Command(OnSelectedCharacter);
        }

      
    }

}