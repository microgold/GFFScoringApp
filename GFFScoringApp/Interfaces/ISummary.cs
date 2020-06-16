using System.Collections.Generic;
using GFFScoringApp.Models;

namespace GFFScoringApp.Interfaces
{
    public interface ISummary
    {
        Character SelectedCharacter { get; set; }
        Smoothie  SelectedSmoothie { get; set; }
        IList<Ingredient> SelectedVeggies { get; set; }
        IList<Ingredient> SelectedFruits { get; set; }
        IList<Ingredient> SelectedBoosts { get; set; }
        IList<Ingredient> SelectedSweeteners { get; set; }
        void AddVeggieSelection(IList<Ingredient> selectedVeggies);
        void AddFruitSelection(IList<Ingredient> selectedFruits);
        void AddBoostSelection(IList<Ingredient> selectedBoosts);
        void AddSweetenerSelection(IList<Ingredient> selectedSweeteners);
        void ClearFruitSelection();
        void ClearVeggieSelection();
        void ClearBoostSelection();
        void ClearSweetenerSelection();
    }
}