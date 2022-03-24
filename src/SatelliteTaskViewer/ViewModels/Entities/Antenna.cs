using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class Antenna : BaseEntity, IDrawable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; } = new FrameViewModel();

        [Reactive]
        public AntennaRenderModel? RenderModel { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (RenderModel == null)
            {
                return;
            }

            if (IsVisible == true)
            {
                if (Frame.State is AntennaAnimator antennaAnimator)
                {
                    if (antennaAnimator.Enable == true)
                    {
                        RenderModel.AbsoluteTargetPostion = antennaAnimator.TargetPosition;
                        renderer.DrawAntenna(dc, RenderModel, antennaAnimator.AbsoluteModelMatrix, scene);
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
