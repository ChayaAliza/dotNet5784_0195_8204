using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    // Class for converting an ID to content (string) based on its value
    class ConvertIdToContent : IValueConverter
    {
        // Converts an ID value to content (string) based on whether it's 0 or not
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value is 0, return "Add", otherwise return "Update"
            return (int)value == 0 ? "Add" : "Update";
        }

        // This method is not implemented as conversion back from content to ID is not supported in this converter
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
