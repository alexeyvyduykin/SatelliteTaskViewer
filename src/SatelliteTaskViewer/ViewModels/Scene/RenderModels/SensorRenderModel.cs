using SatelliteTaskViewer.ViewModels.Data;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class SensorRenderModel : BaseRenderModel
    {
        [Reactive]
        public Scan Scan { get; set; }

        [Reactive]
        public Shoot Shoot { get; set; }
    }
}
