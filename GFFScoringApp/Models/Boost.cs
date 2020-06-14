using System.Windows.Input;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Boost : Ingredient
    {
        public ICommand SelectedBoostCommand { get; set; }

        public Boost()
        {
            SelectedBoostCommand = new Command(OnSelectedBoost);
        }

        private void OnSelectedBoost(object boost)
        {
            MessagingCenter.Send<Boost>(this, "toggledboost");
        }
    }
}
