using SatelliteTaskViewer.Models.Image;
using A = Pfim;

namespace SatelliteTaskViewer.Avalonia.ImageLoader.Pfim
{
    internal class PfimFactory
    {
        public IDdsImage? CreateDdsImage(A.IImage image)
        {
            if (image is A.Dds ddsImage)
            {
                return new PfimDdsImage(ddsImage);
            }

            return null;
        }
    }
}
