using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace wScreenshot.Modules.Base.Converter
{
    public class StringToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder("#");
            foreach (var v in values)
            {
                sb.Append(string.Format("{0:X2}", System.Convert.ToInt32(v)));
            }
            return sb.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            string r = value.ToString();
            if (r.StartsWith("#"))
            {
                r = r.Substring(1);
            }
            try
            {
                List<double> ret = new List<double>();
                while (r.Length > 1)
                {
                    string sub = r.Substring(0, 2);
                    r = r.Substring(2);
                    int tmp = 255;
                    int.TryParse(sub, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out tmp);
                    ret.Add((double)tmp);
                }
                while (ret.Count < targetTypes.Length)
                {
                    ret.Add(255);
                }
                return (from object i in ret
                        select i).Take(targetTypes.Length).ToArray();
            }
            catch (Exception) { }
            return null;
        }
    }
}