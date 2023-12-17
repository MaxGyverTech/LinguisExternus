using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace LinguisExternus.Models.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as string).IsNullOrEmpty() ? Visibility.Visible : Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => throw new System.NotImplementedException();

    }
}
