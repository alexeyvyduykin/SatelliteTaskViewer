using System.Collections.Generic;
using GlmSharp;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class OrbitRenderModel : BaseRenderModel
    {
        [Reactive]
        public IList<dvec3>? Vertices { get; set; }
    }
}
