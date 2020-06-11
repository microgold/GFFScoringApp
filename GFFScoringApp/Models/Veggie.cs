using System.Windows.Input;
using GFFScoringApp.Views;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Veggie : Ingredient
    {
        public ICommand SelectedVeggieCommand { get; set; }

        public Veggie()
        {
            SelectedVeggieCommand = new Command(OnSelectedAVeggie);
        }

        private void OnSelectedAVeggie(object obj)
        {
            MessagingCenter.Send<Veggie>(this, "toggledveggie");
        }
    }
}
