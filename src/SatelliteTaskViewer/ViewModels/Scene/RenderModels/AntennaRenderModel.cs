using GlmSharp;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class AntennaRenderModel : BaseRenderModel
    {
        [Reactive]
        public dvec3 AbsoluteTargetPostion { get; set; }
    }
}
