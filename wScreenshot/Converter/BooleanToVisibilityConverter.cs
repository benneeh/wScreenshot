using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace wScreenshot.Converter
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentException("Value may not b null", "value");
            if (!(value is bool)) throw new ArgumentException("Must b of type bool", "value");

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentException("Value may not b null", "value");
            if (!(value is Visibility)) throw new ArgumentException("Must b of type Visibility", "value");

            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}