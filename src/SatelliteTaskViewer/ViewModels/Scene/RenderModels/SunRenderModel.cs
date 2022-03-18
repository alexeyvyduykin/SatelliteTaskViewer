using SatelliteTaskViewer.ViewModels.Geometry;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class SunRenderModel : BaseRenderModel
    {
        public Mesh Billboard { get; set; }

        public string SunGlowKey { get; set; }
    }
}
