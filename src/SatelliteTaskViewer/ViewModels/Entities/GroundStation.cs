using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class GroundStation : BaseEntity, IDrawable, ITargetable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public RenderModel RenderModel { get; set; }

        public dmat4 InverseAbsoluteModel
        {
            get
            {
                if (Frame.State is IFrameable)
                {
                    if (Frame.State is GroundStationState groundStationData)
                    {
                        var collection = Frame.Parent;
                        var parent = collection.Parent;
                        if (parent.State is EarthAnimator j2000Data)
                        {
                            var modelMatrix = j2000Data.ModelMatrix * groundStationData.ModelMatrix;
                            return modelMatrix.Inverse;
                        }
                    }
                }

                return dmat4.Identity.Inverse;
            }
        }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {
                if (Frame.State is GroundStationState groundStationData)
                {
                    var collection = Frame.Parent;
                    var parent = collection.Parent;
                    if (parent.State is EarthAnimator j2000Data)
                    {
                        var m = j2000Data.ModelMatrix;

                        var groundStationModelMatrix = m * groundStationData.ModelMatrix;

                        renderer.DrawGroundStation(dc, RenderModel, groundStationModelMatrix, scene);
                    }
                }
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
