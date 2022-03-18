using SatelliteTaskViewer.ViewModels.Geometry;

namespace SatelliteTaskViewer.Models
{
    public interface IModelLoader
    {
        Model LoadModel(string path, bool flipUVs);
    }
}
