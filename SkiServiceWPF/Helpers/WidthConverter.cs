using System;
using System.Globalization;
using System.Windows.Data;

namespace SkiServiceWPF.Helpers
{
    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double totalWidth && double.TryParse(parameter.ToString(), out double factor))
            {
                double result = totalWidth * factor - 5; // 5 ist ein kleiner Offset für Padding/Margin
                return result > 0 ? result : 0; // Stellt sicher, dass der Wert nicht negativ ist
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


