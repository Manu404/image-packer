using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImagePacker.Client.Common.Helper
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return ((Visibility)value) == Visibility.Visible ? false : true;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be visibility");

            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
