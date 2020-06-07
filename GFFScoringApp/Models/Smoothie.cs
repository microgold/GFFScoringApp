using System;
using System.Collections.Generic;
using System.Text;

namespace GFFScoringApp.Models
{
    public class Smoothie : Item
    {
        public int MinimumProteinRequirement { get; set; }
        public int MinimumFatRequirement { get; set; }
        public int MinimumSweetnessRequirement { get; set; }
        public int VeggieRequirement { get; set; }
        public int FruitRequirement { get; set; }
        public int SweetnerRequirement { get; set; }
        public int BoostRequirement { get; set; }
    }
}
