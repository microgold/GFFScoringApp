using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GFFScoringApp.ViewModels;
using Xamarin.Forms.Internals;

namespace GFFScoringApp.Models
{
    public class Summary : ISummary
    {
        public int VeggieScore {
            get
            {
                var pureHealthBonus = SelectedVeggies.Select(veggie => veggie.HealthBonus).Sum();
                return pureHealthBonus;
            }
        }
        public int FruitScore
        {
            get
            {
                var pureHealthBonus = SelectedFruits.Select(fruit => fruit.HealthBonus).Sum();
                return pureHealthBonus;
            }
        }

        public int BoostScore
        {
            get
            {
                var pureHealthBonus = SelectedBoosts.Select(boost => boost.HealthBonus).Sum();
                return pureHealthBonus;
            }
        }

        public int CalculateProteinBonus()
        {
            return AllIngredients.Sum(ingredient => ingredient.Protein);
        }

        public int CalculateFatBonus()
        {
            return AllIngredients.Sum(ingredient => ingredient.Protein);
        }

        /// <summary>
        /// if sweetness is over 10, lose the bonus
        /// if its a crappy frappy, sweetness has to be over 15
        /// </summary>
        /// <returns></returns>
        public int CalculateSweetnessBonus()
        {
            return AllIngredients.Sum(ingredient => ingredient.Sweetness);

        }

        private IList<Ingredient> AllIngredients
        {
            get
            {
                IList<Ingredient> allIngredients = SelectedFruits.Union<Ingredient>(SelectedSweeteners)
                    .Union<Ingredient>(SelectedBoosts)
                    .Union<Ingredient>(SelectedVeggies).ToList();
                return allIngredients;
            }
        }

        public int AttributeBonus
        {
            get
            {
                var receiveAttributeSweetBonus = CalculateSweetnessBonus() >= SelectedSmoothie.MinimumSweetnessRequirement &&
                                                 CalculateSweetnessBonus() < SweetnessLimit;
                var receiveAttributeFatBonus = CalculateFatBonus() >= SelectedSmoothie.MinimumFatRequirement;
                var receiveAttributeProteinBonus =  CalculateProteinBonus() >= SelectedSmoothie.MinimumProteinRequirement;

                
              

                var bonus = ((receiveAttributeSweetBonus ? 1 : 0) * 20) +
                            ((receiveAttributeFatBonus ? 1 : 0) * 20) +
                            ((receiveAttributeProteinBonus ? 1 : 0) * 20);

                return bonus;

            }
        }

        private int SweetnessLimit => (SelectedSmoothie.Name == "Crappy Frappe") ? 15 : 10;

        public int TotalScore
        {
            get
            {
                return AttributeBonus + VeggieScore + FruitScore + BoostScore + SweetenerScore;
            }
        }

        public int SweetenerScore
        {
            get
            {
                var pureHealthBonus = SelectedSweeteners.Select(sweetener => sweetener.HealthBonus).Sum();
                return pureHealthBonus;
            }
        }

        public IList<Veggie> SelectedVeggies { get; set; }
        public IList<Fruit> SelectedFruits { get; set; }
        public IList<Boost> SelectedBoosts { get; set; }
        public  IList<Sweetener> SelectedSweeteners { get; set; }
        
        public void AddVeggieSelection(IList<Veggie> selectedVeggies)
        {
            SelectedVeggies.Clear();
            selectedVeggies.ForEach(veggie => SelectedVeggies.Add(veggie));
        }

        public void AddFruitSelection(IList<Fruit> selectedFruits)
        {
            SelectedFruits.Clear();
            selectedFruits.ForEach(fruit => SelectedFruits.Add(fruit));
        }

        public void AddBoostSelection(IList<Boost> selectedBoosts)
        {
            SelectedBoosts.Clear();
            selectedBoosts.ForEach(boost => SelectedBoosts.Add(boost));
        }

        public void AddSweetenerSelection(IList<Sweetener> selectedSweeteners)
        {
            SelectedSweeteners.Clear();
            selectedSweeteners.ForEach(sweetener => SelectedSweeteners.Add(sweetener));
        }

        public Character SelectedCharacter { get; set; }
        public Smoothie SelectedSmoothie { get; set; }

        public Summary()
        {
            SelectedCharacter = new Character();
            SelectedSmoothie = new Smoothie();
            SelectedVeggies = new List<Veggie>();
            SelectedFruits = new List<Fruit>();
            SelectedBoosts = new List<Boost>();
            SelectedSweeteners = new List<Sweetener>();
        }
    }

    public interface ISummary
    {
        Character SelectedCharacter { get; set; }
        Smoothie  SelectedSmoothie { get; set; }
        IList<Veggie> SelectedVeggies { get; set; }
        IList<Fruit> SelectedFruits { get; set; }
        IList<Boost> SelectedBoosts { get; set; }
        IList<Sweetener> SelectedSweeteners { get; set; }
        void AddVeggieSelection(IList<Veggie> selectedVeggies);
        void AddFruitSelection(IList<Fruit> selectedFruits);
        void AddBoostSelection(IList<Boost> selectedBoosts);
        void AddSweetenerSelection(IList<Sweetener> selectedSweeteners);
    }
}