using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SkiServiceWPF.Helpers
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && !string.IsNullOrEmpty(dateString))
            {
                if (DateTime.TryParse(dateString, null, DateTimeStyles.RoundtripKind, out DateTime dateTime))
                {
                    return dateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Debug.WriteLine("DateTimeConverter: Konnte Datum nicht parsen - " + dateString);
                }
            }
            return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
