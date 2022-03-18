using SatelliteTaskViewer.ViewModels.Geometry;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class RenderModel : BaseRenderModel
    {
        //public FrameRenderModel Frame { get; set; }

        public Model Model { get; set; }

        public double Scale { get; set; }
    }
}
