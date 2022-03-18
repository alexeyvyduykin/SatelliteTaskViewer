using System;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public abstract class BaseSatelliteEvent : ViewModelBase
    {
        [Reactive]
        public DateTime Epoch { get; set; }

        [Reactive]
        public double BeginTime { get; set; }

        [Reactive]
        public double EndTime { get; set; }

        public DateTime Begin => Epoch.AddSeconds(BeginTime);

        public TimeSpan Duration => Epoch.AddSeconds(EndTime) - Epoch.AddSeconds(BeginTime);
    }
}
