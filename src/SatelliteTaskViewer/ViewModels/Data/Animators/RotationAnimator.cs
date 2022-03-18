using System.Collections.Generic;
using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class RotationAnimator : BaseState, IAnimator
    {
        private readonly IEventList<RotationInterval> _rotationEvents;

        public RotationAnimator(RotationData data)
        {
            _rotationEvents = Create(data.Rotations);
        }

        [Reactive]
        public double GamDEG { get; protected set; }

        private static IEventList<RotationInterval> Create(IList<RotationRecord> rotations)
        {
            var rotationEvents = new EventList<RotationInterval>(EventMissMode.LastActive);

            double lastAngle = 0.0;

            foreach (var item in rotations)
            {
                rotationEvents.Add(new RotationInterval(item.BeginTime, item.EndTime, lastAngle, item.Angle));

                lastAngle = item.Angle;
            }

            return rotationEvents;
        }

        public void Animate(double t)
        {
            var activeInterval = _rotationEvents.ActiveInterval(t);

            GamDEG = (activeInterval != default) ? activeInterval.Animate(t).Angle : 0.0;

            ModelMatrix = dmat4.Rotate(-glm.Radians(GamDEG), dvec3.UnitX);
        }
    }
}
