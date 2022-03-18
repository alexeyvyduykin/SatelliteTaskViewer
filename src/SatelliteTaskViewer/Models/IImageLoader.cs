using System.Collections.Generic;
using SatelliteTaskViewer.Models.Image;

namespace SatelliteTaskViewer.Models
{
    public interface IImageLoader
    {
        IDdsImage? LoadDdsImageFromFile(string path);

        IEnumerable<IDdsImage?> LoadDdsImageFromFiles(IEnumerable<string> paths);
    }
}
