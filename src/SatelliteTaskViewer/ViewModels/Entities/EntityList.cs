using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Entities;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.Models.Scene;
using System.Collections.Immutable;
using System.Linq;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public class EntityList : BaseEntity, IDrawable, ICollection<BaseEntity>
    {
        [Reactive]
        public ImmutableArray<BaseEntity> Values { get; set; }

        public void DrawShape(object dc, IRenderContext renderer, ISceneState scene)
        {
            if (IsVisible == true)
            {
                var first = Values.FirstOrDefault();

                if (first is GroundObject groundObject)
                {
                    if (groundObject.IsVisible == true && groundObject.RenderModel != null)
                    {
                        var collection = groundObject.Frame.Parent;

                        if (collection == null)
                        {
                            return;
                        }

                        var matrices = collection.Children.Select(s => s.State.AbsoluteModelMatrix);
                        renderer.DrawGroundObjectList(dc, groundObject.RenderModel, matrices, scene);
                    }
                }
                else if (first is GroundStation groundStation)
                {
                    if (groundStation.IsVisible == true && groundStation.RenderModel != null)
                    {
                        var collection = groundStation.Frame.Parent;

                        if (collection == null)
                        {
                            return;
                        }

                        foreach (var item in collection.Children)
                        {
                            if (item == null || item.State == null)
                            {
                                continue;
                            }

                            var matrix = item.State.AbsoluteModelMatrix;
                            renderer.DrawGroundStation(dc, groundStation.RenderModel, matrix, scene);
                        }
                    }
                }
                else if (first is Retranslator retranslator)
                {
                    if (retranslator.IsVisible == true && retranslator.RenderModel != null)
                    {
                        var collection = retranslator.Frame.Parent;

                        if (collection == null)
                        {
                            return;
                        }

                        foreach (var item in collection.Children)
                        {
                            if (item == null || item.State == null)
                            {
                                continue;
                            }

                            var matrix = item.State.ModelMatrix;
                            renderer.DrawRetranslator(dc, retranslator.RenderModel, matrix, scene);
                        }
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
