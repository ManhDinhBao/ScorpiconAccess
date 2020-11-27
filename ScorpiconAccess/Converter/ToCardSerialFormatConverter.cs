using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScorpiconAccess
{
    public class ToCardSerialFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string strValue = value.ToString();
                string strNewValue = "";
                for (int i = 0; i < strValue.Length; i++)
                {                   
                    if (i % 4 == 0 && i>0)
                    {
                        strNewValue += " ";
                    }
                    strNewValue += strValue[i];
                }

                return strNewValue;
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
