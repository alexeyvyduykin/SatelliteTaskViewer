using SatelliteTaskViewer.Models.Image;
using A = Pfim;

namespace SatelliteTaskViewer.Avalonia.ImageLoader.Pfim
{
    internal class PfimDdsPixelFormat : IDdsPixelFormat
    {
        private readonly A.DdsPixelFormat _ddsPixelFormat;
        private readonly DdsPixelFormatFlags _flags;
        private readonly CompressionAlgorithm _fourCC;

        public PfimDdsPixelFormat(A.DdsPixelFormat ddsPixelFormat)
        {
            _ddsPixelFormat = ddsPixelFormat;
            _flags = ddsPixelFormat.PixelFormatFlags.Convert();
            _fourCC = ddsPixelFormat.FourCC.Convert();
        }

        public uint Size => _ddsPixelFormat.Size;

        public DdsPixelFormatFlags Flags => _flags;

        public CompressionAlgorithm FourCC => _fourCC;

        public uint RGBBitCount => _ddsPixelFormat.RGBBitCount;

        public uint RBitMask => _ddsPixelFormat.RBitMask;

        public uint GBitMask => _ddsPixelFormat.GBitMask;

        public uint BBitMask => _ddsPixelFormat.BBitMask;

        public uint ABitMask => _ddsPixelFormat.ABitMask;
    }
}
