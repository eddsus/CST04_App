using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFDashboard.Helpers.Converter
{
    public class BoolToPublishConverter : IValueConverter
    {
        String publish = "Publish";
        String undo = "Undo";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool temp = (bool)value;
            if (temp==true)
            {
                return undo;
            }
            else
            {
                return publish;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
