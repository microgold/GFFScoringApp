using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GFFScoringApp.Interfaces;
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
            return AllIngredients.Sum(ingredient => ingredient.Fat);
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

        public IList<Ingredient> SelectedVeggies { get; set; }
        public IList<Ingredient> SelectedFruits { get; set; }
        public IList<Ingredient> SelectedBoosts { get; set; }
        public  IList<Ingredient> SelectedSweeteners { get; set; }

        public void ClearVeggieSelection()
        {
            SelectedVeggies.Clear();
        }

        public void ClearFruitSelection()
        {
            SelectedFruits.Clear();
        }

        public void ClearBoostSelection()
        {
            // don't clear out substitutes
            SelectedBoosts.Clear();
        }

        public void ClearSweetenerSelection()
        {
            // don't clear out substitutes
            SelectedSweeteners.Clear();
        }


        public void AddVeggieSelection(IList<Ingredient> selectedVeggies)
        {
            selectedVeggies.ForEach(veggie => SelectedVeggies.Add(veggie));
        }


        public void AddFruitSelection(IList<Ingredient> selectedFruits)
        {
            selectedFruits.ForEach(fruit => SelectedFruits.Add(fruit));
        }

        public void AddBoostSelection(IList<Ingredient> selectedBoosts)
        {
            selectedBoosts.ForEach(boost => SelectedBoosts.Add(boost));
        }

        public void AddSweetenerSelection(IList<Ingredient> selectedSweeteners)
        {
            selectedSweeteners.ForEach(sweetener => SelectedSweeteners.Add(sweetener));
        }

        public Character SelectedCharacter { get; set; }
        public Smoothie SelectedSmoothie { get; set; }

        public Summary()
        {
            SelectedCharacter = new Character();
            SelectedSmoothie = new Smoothie();
            SelectedVeggies = new List<Ingredient>();
            SelectedFruits = new List<Ingredient>();
            SelectedBoosts = new List<Ingredient>();
            SelectedSweeteners = new List<Ingredient>();
        }
    }
}