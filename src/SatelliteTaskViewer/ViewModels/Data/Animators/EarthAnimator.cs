using System;
using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class EarthAnimator : BaseState, IAnimator
    {
        private readonly double _angleDeg0;
        private readonly DateTime _epoch;

        public EarthAnimator(EarthData data)
        {
            _angleDeg0 = data.AngleDeg;
            _epoch = data.Epoch;
        }

        [Reactive]
        public double AngleDEG { get; protected set; }

        public void Animate(double t)
        {
            double w = 7.292115085e-5;
            AngleDEG = _angleDeg0 + glm.Degrees(w * t);
            while (AngleDEG > 360.0)
            {
                AngleDEG -= 360.0;
            }

            ModelMatrix = dmat4.Rotate(glm.Radians(AngleDEG), new dvec3(0.0, 1.0, 0.0));
        }
    }
}
