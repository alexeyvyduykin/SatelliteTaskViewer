using GlmSharp;
using SatelliteTaskViewer.Models.Scene;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public abstract class BaseCamera : ViewModelBase, ICamera
    {
        public abstract dmat4 ViewMatrix { get; }

        [Reactive]
        public dvec3 Eye { get; set; }

        [Reactive]
        public dvec3 Target { get; set; }

        [Reactive]
        public dvec3 Up { get; set; }

        public dvec3 Forward => (Target - Eye).Normalized;

        public dvec3 Right => dvec3.Cross(Forward, Up).Normalized;

        public abstract void LookAt(dvec3 eye, dvec3 target, dvec3 up);
    }
}
