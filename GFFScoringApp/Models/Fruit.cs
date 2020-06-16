using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Fruit : Ingredient
    {
  
        public ICommand SelectedFruitCommand { get; set; }
        public bool IsBerry { get; set; }

        public Fruit()
        {
            SelectedFruitCommand = new Command(OnSelectedFruit);
        }

        private void OnSelectedFruit(object obj)
        {
            OnPropertyChanged(nameof(ShowSweetenerChoice));
            MessagingCenter.Send<Fruit>(this, "toggledfruit");
        }

    }

}

