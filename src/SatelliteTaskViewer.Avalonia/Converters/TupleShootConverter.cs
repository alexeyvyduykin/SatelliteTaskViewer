using System;
using System.Globalization;
using Avalonia.Data.Converters;
using GlmSharp;

namespace SatelliteTaskViewer.Avalonia.Converters
{
    public class TupleShootConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                throw new Exception();
            }

            var shoot = ((dvec3, dvec3, dvec3, dvec3))value;

            var parameterString = (string)parameter;
            if (int.TryParse(parameterString, out int index))
            {
                if (index >= 0 && index < 4)
                {
                    var arr = new double[][] { shoot.Item1.Values, shoot.Item2.Values, shoot.Item3.Values, shoot.Item4.Values };
                    return string.Format("{0: 0.0;-0.0}; {1: 0.0;-0.0}; {2: 0.0;-0.0}", arr[index][0], arr[index][1], arr[index][2]);
                }
            }

            // //  if (shoot != null)
            // //  {                
            // string parameterString = parameter as string;
            //     if (!string.IsNullOrEmpty(parameterString))
            //     {
            //         string[] parameters = parameterString.Split(new char[] { '|' });

            //         if (int.TryParse(parameters[0], out int i) && int.TryParse(parameters[1], out int j))
            //         {
            //             if(i >= 0 && i < 4 && j >= 0 && j < 3)
            //             {
            //                 var arr = new double[][] { shoot.Item1.Values, shoot.Item2.Values, shoot.Item3.Values, shoot.Item4.Values };
            //                 return string.Format("{0:F3}", arr[i][j]);
            //             }
            //         }
            //     }
            //// }

            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
