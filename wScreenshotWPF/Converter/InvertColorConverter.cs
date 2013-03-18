﻿using System;
using System.Windows.Data;
using System.Windows.Media;

namespace wScreenshot.Converter
{
    public class InvertColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            return new Color()
            {
                A = c.A,
                R = (byte)(255 - c.R),
                G = (byte)(255 - c.G),
                B = (byte)(255 - c.B)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color c = (Color)value;
            return new Color()
            {
                A = c.A,
                R = (byte)(255 - c.R),
                G = (byte)(255 - c.G),
                B = (byte)(255 - c.B)
            };
        }
    }
}