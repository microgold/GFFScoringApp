namespace GFFScoringApp.Models
{
    public enum IngredientCategory
    {
        Veggie,
        Fruit,
        Sweetner,
        Boost
    }

    public class Ingredient : Item
    {
        public bool IsSuperFood { get; set; }

        public bool IsSelected { get; set; }

        public IngredientCategory Category { get; set; }

        public int HealthBonus { get; set; }

        public int ExtraHealthBonus { get; set; }

        public int Protein { get; set; }

        public int Fat { get; set; }

        public int Sweetness { get; set; }

    }
}
