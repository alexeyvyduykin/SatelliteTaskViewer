using GlmSharp;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Earth : BaseEntity, IDrawable, ITargetable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; } = new FrameViewModel();

        [Reactive]
        public EarthRenderModel? RenderModel { get; set; }

        public dmat4 InverseAbsoluteModel => dmat4.Identity.Inverse;

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (RenderModel == null || Frame.State == null)
            {
                return;
            }

            if (IsVisible == true)
            {
                renderer.DrawEarth(dc, RenderModel, Frame.State.ModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
