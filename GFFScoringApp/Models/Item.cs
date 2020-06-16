using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GFFScoringApp.Annotations;
using Xamarin.Forms;

namespace GFFScoringApp.Models
{
    public class Item : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ImageSource ImageUrl { get; set; }
        public string Location { get; set; }

        public string Details { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}