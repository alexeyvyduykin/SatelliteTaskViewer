using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Sun : BaseEntity, IDrawable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public SunRenderModel RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {
                scene.LightPosition = new dvec4(Frame.State.ModelMatrix.Column3.ToDvec3() / 1000.0, 1.0);

                renderer.DrawSun(dc, RenderModel, Frame.State.ModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
