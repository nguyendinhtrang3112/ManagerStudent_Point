using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp2.Converters
{
    public class AlternatingRowColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                return index % 2 == 0 ? Colors.White : Colors.AliceBlue;
            }

            return Colors.White;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
