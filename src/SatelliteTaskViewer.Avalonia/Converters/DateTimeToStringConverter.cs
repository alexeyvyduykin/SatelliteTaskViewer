using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SatelliteTaskViewer.Avalonia.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        private readonly string _format = "dd-MMM-yyyy hh:mm:ss";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                return dt.ToString(_format, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            }

            return DateTime.MinValue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanToStringConverter : IValueConverter
    {
        private readonly string _format = @"mm\:ss";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(_format, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            }

            return DateTime.MinValue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
