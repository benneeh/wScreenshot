using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace wScreenshot.Converter
{
    public class StringToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var sb = new StringBuilder("#");
            foreach (object v in values)
            {
                sb.Append(string.Format("{0:X2}", System.Convert.ToInt32(v)));
            }
            return sb.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string r = value.ToString();
            if (r.StartsWith("#"))
            {
                r = r.Substring(1);
            }
            try
            {
                var ret = new List<double>();
                while (r.Length > 1)
                {
                    string sub = r.Substring(0, 2);
                    r = r.Substring(2);
                    int tmp = 255;
                    int.TryParse(sub, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out tmp);
                    ret.Add(tmp);
                }
                while (ret.Count < targetTypes.Length)
                {
                    ret.Add(255);
                }
                return (from object i in ret
                    select i).Take(targetTypes.Length).ToArray();
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}