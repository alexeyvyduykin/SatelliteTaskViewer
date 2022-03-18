using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Sensor : BaseEntity, IDrawable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public SensorRenderModel RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {
                if (Frame.State is SensorAnimator state)
                {
                    if (state.Enable == true)
                    {
                        RenderModel.Shoot = state.Shoot;
                        RenderModel.Scan = state.Scan;

                        renderer.DrawSensor(dc, RenderModel, dmat4.Identity, scene);
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
