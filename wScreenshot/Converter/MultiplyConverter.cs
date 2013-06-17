using System;
using System.Globalization;
using System.Windows.Data;

namespace wScreenshot.Converter
{
    public class MultiplyConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The type of the binding target property.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="ArgumentException">throws a new "ArgumentException"</exception>
        /// <exception cref="ArgumentNullException">throws a new "ArgumentNullException"</exception>
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType", "The targetType parameter must be set.");
            }
            decimal mul = 1;
            int round = 0;
            decimal retVal = 0;
            if (parameter is string)
            {
                string[] split = parameter.ToString().Replace("'", "").Split(';');
                if (split.Length >= 1)
                {
                    mul = decimal.Parse(split[0]);
                }
                if (split.Length > 1)
                {
                    round = int.Parse(split[1]);
                }
            }

            decimal.TryParse(value.ToString(), out retVal);
            return Math.Round(retVal*mul, round);
        }

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">throws a new "NotSupportedException"</exception>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType", "The targetType parameter must be set.");
            }
            if (value is int)
            {
                return (decimal) (((int) value)/100);
            }
            return 0;
        }
    }
}