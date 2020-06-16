namespace GFFScoringApp.Interfaces
{
    internal interface ICheckIngredientRequirements<T>
    {
        bool DoesIngredientsMeetSmoothieRequirements(T ingredient);
    }
}