using SatelliteTaskViewer.Models.Image;
using A = Pfim;

namespace SatelliteTaskViewer.Avalonia.ImageLoader.Pfim
{
    internal class PfimDdsImage : IDdsImage
    {
        private readonly A.Dds _ddsImage;
        private readonly IDdsImageHeader _ddsImageHeader;

        public PfimDdsImage(A.Dds ddsImage)
        {
            _ddsImage = ddsImage;
            _ddsImageHeader = new PfimDdsImageHeader(ddsImage.Header);
        }

        public int Width => _ddsImage.Width;

        public int Height => _ddsImage.Height;

        public byte[] Data => _ddsImage.Data;

        public bool Compressed => _ddsImage.Compressed;

        public IDdsImageHeader Header => _ddsImageHeader;
    }
}
