using GlmSharp;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using System.Linq;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Orbit : BaseEntity, IDrawable
    {
        public Orbit()
        {
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(RenderModel) || e.PropertyName == nameof(Frame))
                {
                    if (RenderModel is not null && Frame is not null && Frame.State is not null && Frame.State is OrbitState state)
                    {
                        RenderModel.Vertices = state.Vertices.Select(s => new dvec3(s.x, s.y, s.z)).ToList();
                    }
                }
            };
        }

        [Reactive]
        public FrameViewModel Frame { get; set; } = new FrameViewModel();

        [Reactive]
        public OrbitRenderModel? RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (RenderModel == null || Frame.State == null)
            {
                return;
            }

            if (IsVisible == true)
            {
                renderer.DrawOrbit(dc, RenderModel, Frame.State.ModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
