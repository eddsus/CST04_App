using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFDashboard.Helpers.Converter
{
    public class BoolToToggleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String toggleOn = "ToggleSwitch";
            String toggleOff = "ToggleSwitchOff";

            bool temp = (bool)value;
            if (temp)
            {
                return toggleOn;

            }
            else
            {
                return toggleOff;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
