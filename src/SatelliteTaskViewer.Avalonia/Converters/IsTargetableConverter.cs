using Avalonia.Data.Converters;
using SatelliteTaskViewer.Models;
using System;
using System.Globalization;

namespace SatelliteTaskViewer.Avalonia.Converters
{
    public class IsTargetableConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null && value is ITargetable;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
