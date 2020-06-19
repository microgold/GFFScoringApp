using System;
using GFFScoringApp.Enums;
using GFFScoringApp.ViewModels;

namespace GFFScoringApp.Models
{
    public class Ingredient : Item
    {
        public Color Color { get; set; }
        public bool IsSuperFood { get; set; }
        public bool IsBerry { get; set; }

        public bool IsSelected { get; set; }

        public bool CanBeSweetener { get; set; }

        public bool ShowSweetenerChoice => IsSelected && CanBeSweetener;

        public bool ShowBoostChoice => IsSelected && CanBeBoost;

        public bool UseAsSweetener { get; set; }

        public bool CanBeBoost { get; set; }

        public bool UseAsBoost { get; set; }

        public IngredientCategory Category { get; set; }

        public int HealthBonus { get; set; }

        public int ExtraHealthBonus { get; set; }

        public Action<Summary> ExtraScoreAction { get; set; }


        public int Protein { get; set; }

        public int Fat { get; set; }

        public int Sweetness { get; set; }

    }
}
