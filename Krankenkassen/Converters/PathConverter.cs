using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.Converters;

public class PathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return IsValidPath(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return IsValidPath(value);
    }
    /// <summary>
    /// Prüft, ob ein gewählter Pfad ein gültiger csv-Pfad ist
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static bool IsValidPath(object value)
    {
        if (value is null) return false;
        var path = value.ToString();
        if(string.IsNullOrEmpty(path)) return false;
        if(Path.GetExtension(path) != ".csv") return false;
        if (Path.GetInvalidPathChars().Any(c=> path.Contains(c))) return false;
        return Path.IsPathRooted(path);
    }
}
