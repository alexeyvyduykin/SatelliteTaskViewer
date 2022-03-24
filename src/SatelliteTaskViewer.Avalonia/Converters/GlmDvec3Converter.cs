using System;
using System.Globalization;
using Avalonia.Data.Converters;
using GlmSharp;

namespace SatelliteTaskViewer.Avalonia.Converters
{
    public class GlmDvec3Converter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is dvec3 vec)
            {
                //string str = string.Empty;

                //str += (vec[0] >= 0.0) ? string.Format("+{0:0.0}; ", vec[0]) : string.Format("-{0:0.0}; ", Math.Abs(vec[0]));
                //str += (vec[1] >= 0.0) ? string.Format("+{0:0.0}; ", vec[1]) : string.Format("-{0:0.0}; ", Math.Abs(vec[1]));
                //str += (vec[2] >= 0.0) ? string.Format("+{0:0.0}",   vec[2]) : string.Format("-{0:0.0}", Math.Abs(vec[2]));

                //return str;

                return string.Format("{0: 0.0;-0.0}; {1: 0.0;-0.0}; {2: 0.0;-0.0}", vec[0], vec[1], vec[2]);
            }

            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
