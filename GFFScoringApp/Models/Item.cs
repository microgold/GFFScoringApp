using System;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageSource ImageUrl { get; set; }
        public string Location { get; set; }

        public string Details { get; set; }
    }
}