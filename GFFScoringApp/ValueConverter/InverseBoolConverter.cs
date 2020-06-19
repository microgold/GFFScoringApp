using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GFFScoringApp.ValueConverter
{
public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !((bool)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
        //throw new NotImplementedException();
    }


    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
}