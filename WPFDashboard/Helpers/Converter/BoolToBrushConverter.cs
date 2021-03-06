﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFDashboard.Converter
{
    public class BoolToBrushConverter : IValueConverter
    {
        //Brush green = new SolidColorBrush(Colors.Green);
        //Brush red = new SolidColorBrush(Colors.Red);
        String on = "Wifi";
        String off = "WifiOff";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool temp = (bool)value;
            if (temp == true)
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
