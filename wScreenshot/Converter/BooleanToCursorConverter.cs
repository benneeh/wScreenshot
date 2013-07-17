using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace wScreenshot.Converter
{
    public class BooleanToCursorConverter : IValueConverter
    {
        private Cursor _whenTrueCursor = Cursors.Hand;
        private Cursor _whenFalseCursor = Cursors.Arrow;

        public Cursor WhenTrueCursor
        {
            get { return _whenTrueCursor; }
            set { _whenTrueCursor = value; }
        }

        public Cursor WhenFalseCursor
        {
            get { return _whenFalseCursor; }
            set { _whenFalseCursor = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentException("Value may not b null", "value");
            if (!(value is bool)) throw new ArgumentException("Must b of type bool", "value");

            return (bool)value ? WhenTrueCursor : WhenFalseCursor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentException("Value may not b null", "value");
            if (!(value is Cursor)) throw new ArgumentException("Must b of type Cursor", "value");

            return (Cursor)value == WhenTrueCursor ? true : false;
        }
    }
}