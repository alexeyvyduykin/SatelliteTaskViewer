using GlmSharp;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class GroundObject : BaseEntity, IDrawable, ITargetable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; } = new FrameViewModel();

        [Reactive]
        public GroundObjectRenderModel? RenderModel { get; set; }

        public dmat4 InverseAbsoluteModel
        {
            get
            {
                if (Frame.State is IFrameable)
                {
                    if (Frame.State is GroundObjectState groundObjectState)
                    {
                        var collection = Frame.Parent ?? throw new System.Exception();
                        var parent = collection.Parent;
                        if (parent != null && parent.State is EarthAnimator j2000Data)
                        {
                            var modelMatrix = j2000Data.ModelMatrix * groundObjectState.ModelMatrix;
                            return modelMatrix.Inverse;
                        }
                    }
                }

                return dmat4.Identity.Inverse;
            }
        }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (RenderModel == null)
            {
                return;
            }

            if (IsVisible == true)
            {
                if (Frame.State is GroundObjectState groundObjectState)
                {
                    var collection = Frame.Parent;

                    if (collection == null)
                    {
                        return;
                    }

                    var parent = collection.Parent;
                    if (parent != null && parent.State is EarthAnimator j2000Data)
                    {
                        var m = j2000Data.ModelMatrix;

                        var matrix = m * groundObjectState.ModelMatrix;

                        renderer.DrawGroundObject(dc, RenderModel, matrix, scene);
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
