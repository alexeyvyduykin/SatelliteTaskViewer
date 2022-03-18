using System.Runtime.InteropServices;

namespace SatelliteTaskViewer.Avalonia.Renderer.OpenTK.Core
{
    internal static class SizeInBytes<T>
    {
        public static readonly int Value = Marshal.SizeOf(typeof(T));
    }
}
