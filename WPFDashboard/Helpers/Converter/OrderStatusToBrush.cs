using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFDashboard.Helpers.Converter
{
    public class OrderStatusToBrush : IValueConverter
    {
        Brush red = new SolidColorBrush(Colors.LightCoral);
        Brush yellow = new SolidColorBrush(Colors.LightGoldenrodYellow);
        Brush green = new SolidColorBrush(Colors.LightGreen);
        Brush white= new SolidColorBrush(Colors.White);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String temp = (String)value;
            switch (temp)
            {
                case "New":
                    return white;
                case "Delayed":
                    return yellow;
                case "Paused":
                    return red;
                default:
                    return green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
