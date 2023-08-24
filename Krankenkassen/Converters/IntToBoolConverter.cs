using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.Converters;

public class IntToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertToBool(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertToBool(value);
    }
    private static bool ConvertToBool(object value)
    {
        var parse = int.TryParse(value.ToString(), out int count);
        return parse && count > 0;
    }
}
