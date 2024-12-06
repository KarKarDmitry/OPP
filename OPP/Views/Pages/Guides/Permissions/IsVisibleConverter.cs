using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OPP.Views.Pages.Guides.Permissions
{
    public class IsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEditable = value != null && (bool)value;
            return isEditable ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
