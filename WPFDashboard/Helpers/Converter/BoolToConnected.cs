using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFDashboard.Helpers.Converter
{
    public class BoolToConnected : IValueConverter
    {
        String on = "Wifi";
        String off = "WifiOff";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool temp = (bool)value;
            if (temp==true)
            {
                return on;
            }
            else
            {
                return off;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
