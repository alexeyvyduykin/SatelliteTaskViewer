using System;

namespace SatelliteTaskViewer.Avalonia.Renderer.OpenTK.Core
{
    internal static class ArraySizeInBytes
    {
        public static int Size<T>(T[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            return values.Length * SizeInBytes<T>.Value;
        }
    }

}
