using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class GroundObject : BaseEntity, IDrawable, ITargetable
    {
        [Reactive]
        public FrameViewModel Frame { get; set; }

        [Reactive]
        public GroundObjectRenderModel RenderModel { get; set; }

        public dmat4 InverseAbsoluteModel
        {
            get
            {
                if (Frame.State is IFrameable)
                {
                    if (Frame.State is GroundObjectState groundObjectState)
                    {
                        var collection = Frame.Parent;
                        var parent = collection.Parent;
                        if (parent.State is EarthAnimator j2000Data)
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
            if (IsVisible == true)
            {
                if (Frame.State is GroundObjectState groundObjectState)
                {
                    var collection = Frame.Parent;
                    var parent = collection.Parent;
                    if (parent.State is EarthAnimator j2000Data)
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
