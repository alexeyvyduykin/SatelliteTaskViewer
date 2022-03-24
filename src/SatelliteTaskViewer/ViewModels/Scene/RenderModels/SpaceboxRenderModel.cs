using SatelliteTaskViewer.ViewModels.Geometry;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class SpaceboxRenderModel : BaseRenderModel
    {
        public Mesh? Mesh { get; set; }
        public string SpaceboxCubemapKey { get; set; } = string.Empty;
    }
}
