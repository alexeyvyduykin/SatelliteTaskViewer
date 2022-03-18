using System;

namespace TimelineChart.Core
{
    public class TrackerEventArgs : EventArgs
    {
        public TrackerHitResult? HitResult { get; set; }
    }
}
