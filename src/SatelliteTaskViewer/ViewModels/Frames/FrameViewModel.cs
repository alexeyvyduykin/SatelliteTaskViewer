using System.Collections.Immutable;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class FrameViewModel : BaseContainerViewModel, IDrawable
    {
        [Reactive]
        public FrameViewModel Parent { get; set; }

        [Reactive]
        public ImmutableArray<FrameViewModel> Children { get; set; }

        [Reactive]
        public BaseState State { get; set; }

        [Reactive]
        public FrameRenderModel RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true && State is not null)
            {
                renderer.DrawFrame(dc, RenderModel, State.AbsoluteModelMatrix, scene);
            }
        }

        public bool Invalidate(IRenderContext renderer)
        {
            return false;
        }
    }
}
