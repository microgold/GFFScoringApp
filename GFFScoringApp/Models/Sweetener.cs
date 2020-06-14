using System.Windows.Input;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Sweetener : Ingredient
    {

        public ICommand SelectedSweetenerCommand { get; set; }
        public bool CanBeSweetner { get; set; }

        public Sweetener()
        {
            SelectedSweetenerCommand = new Command(OnSelectedSweetener);
        }

        private void OnSelectedSweetener(object obj)
        {
            MessagingCenter.Send<Sweetener>(this, "toggledsweetener");
        }
    }
}
