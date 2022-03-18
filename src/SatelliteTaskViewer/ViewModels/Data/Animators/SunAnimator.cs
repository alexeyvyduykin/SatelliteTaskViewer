using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class SunAnimator : BaseState, IAnimator
    {
        private readonly dvec3 _position0;
        private readonly dvec3 _position1;
        private readonly double _timeBegin;
        private readonly double _timeEnd;

        public SunAnimator(SunData data)
        {
            _position0 = data.Position0;
            _position1 = data.Position1;
            _timeBegin = data.TimeBegin;
            _timeEnd = data.TimeEnd;
        }

        [Reactive]
        public dvec3 Position { get; protected set; }

        private dvec3 GetPosition(double t)
        {
            double coef = t / (_timeEnd - _timeBegin);

            var p = _position0 + (_position1 - _position0) * coef;

            return p;
        }

        public void Animate(double t)
        {
            Position = GetPosition(t);

            ModelMatrix = dmat4.Translate(Position);
        }
    }
}
