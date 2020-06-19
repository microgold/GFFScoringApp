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



        /// <summary>
        /// if sweetness is over 10, lose the bonus
        /// if its a crappy frappy, sweetness has to be over 15
        /// </summary>
        /// <returns></returns>

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
                var receiveAttributeSweetBonus = SweetnessScore >= SelectedSmoothie.MinimumSweetnessRequirement &&
                                                 SweetnessScore < SweetnessLimit;
                var receiveAttributeFatBonus = FatScore >= SelectedSmoothie.MinimumFatRequirement;
                var receiveAttributeProteinBonus =  ProteinScore >= SelectedSmoothie.MinimumProteinRequirement;

                
              

                var bonus = ((receiveAttributeSweetBonus ? 1 : 0) * 20) +
                            ((receiveAttributeFatBonus ? 1 : 0) * 20) +
                            ((receiveAttributeProteinBonus ? 1 : 0) * 20);

                return bonus;

            }
        }

        public int HolyKaleBonus
        {
            get
            {
                return HasHolyKaleMixup ? 30 : 0;
            }
        }


        private int RainbowGlowBonus
        {
            get
            {
                if (SelectedSmoothie.Name != "Rainbow Glow") return 0;
                var ingredientColorCounts =  AllIngredients.GroupBy(ingredient => ingredient.Color).Select(group => new
                {
                    Metric = group.Key,
                    Count = group.Count()
                });

              return ingredientColorCounts.Any(count => count.Count > 1) ? 0 : 10;
            }

        }


        private int SuperFoodBonus
        {
            get
            {
                return AllIngredients.Count(ingredient => ingredient.IsSuperFood) >= 3 ? 30 : 0;
            }
        }

        private int BerryBoostBonus
        {
            get
            {
                if (SelectedSmoothie.Name != "Berryboost Blitz") return 0;
                return SelectedFruits.Count(fruit => fruit.IsBerry) > 0 ? 10 : 0;
            }
        }

        private int SuperFoodSallieBonus
        {
            get
            {
                if (SelectedSmoothie.Name != "Superfood Sallie") return 0;
                return AllIngredients.Count(ingredient => ingredient.IsSuperFood) >= 2 ? 20 : 0;
            }
        }

        private int BlueberryBonus
        {
            get
            {
                return SelectedFruits.Count(ingredient => ingredient.Name == "blueberry") == 1 ? 20 : 0;
            }
        }

        public int ExtraHealthBonus
        {
            get
            {
                // calculate if they have 3 or more superfoods
                var extraBonus = SuperFoodBonus + BlueberryBonus + RainbowGlowBonus + BerryBoostBonus + SuperFoodSallieBonus + HolyKaleBonus;

                return extraBonus;
            }
        }


        private int SweetnessLimit => (SelectedSmoothie.Name == "Crappy Frappe") ? 15 : 10;

        public int TotalScore
        {
            get
            {
                return AttributeBonus + VeggieScore + FruitScore + BoostScore + SweetenerScore + ExtraHealthBonus;
            }
        }

        public int SweetnessScore
        {
            get
            {
                return AllIngredients.Select(ingredient => ingredient.Sweetness).Sum();
            }
        }

        public int FatScore
        {
            get
            {
                return AllIngredients.Select(ingredient => ingredient.Fat).Sum();
            }
        }
        public int ProteinScore
        {
            get
            {
                return AllIngredients.Select(ingredient => ingredient.Protein).Sum();
            }
        }

        public bool HasHolyKaleMixup { get; set; }

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