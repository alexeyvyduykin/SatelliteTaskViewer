using System;
using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using SatelliteTaskViewer.Models.Data;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class RetranslatorAnimator : BaseState, IAnimator
    {
        private readonly IList<(double x, double y, double z, double u)> _records;
        private readonly double _timeBegin;
        private readonly double _timeEnd;
        private readonly double _timeStep;

        public RetranslatorAnimator(RetranslatorData data)
        {
            _records = data.Records.Select(s => (s[0], s[1], s[2], s[3])).ToList();
            _timeBegin = data.TimeBegin;
            _timeEnd = data.TimeEnd;
            _timeStep = data.TimeStep;
        }

        [Reactive]
        public dmat4 Translate { get; protected set; }

        [Reactive]
        public dmat4 Rotation { get; protected set; }

        [Reactive]
        public dvec3 Position { get; protected set; }

        private dvec3 GetPosition(double t)
        {
            double tCur = t;// base.LocalTime;

            int n = (int)Math.Floor(tCur / _timeStep);

            //  dvec3 pn = positions[n];
            //  dvec3 pk = positions[n + 1];

            dvec3 p;
            double OrbitRadius;

            if (n == _records.Count - 1) // для времени t равного Tend
            {
                p = new dvec3(_records[n].y, _records[n].z, _records[n].x);

                OrbitRadius = p.Length;
            }
            else
            {
                dvec3 pn = new dvec3(_records[n].y, _records[n].z, _records[n].x);
                dvec3 pk = new dvec3(_records[n + 1].y, _records[n + 1].z, _records[n + 1].x);

                OrbitRadius = pn.Length;

                double coef = (tCur - _timeStep * n) / _timeStep;

                p = pn + (pk - pn) * coef;
            }

            p = glm.Normalized(p);

            p *= OrbitRadius;

            return p;
        }

        public void Animate(double t)
        {
            var pos = GetPosition(t);
            var translation = dmat4.Translate(pos);

            var v1 = dvec3.UnitZ;
            var v2 = -glm.Normalized(pos);
            var angle = Math.Acos(glm.Dot(v1, v2));

            if (v1.x * v2.z - v1.z * v2.x < 0)
            {
                angle = -angle;
            }

            var rotation = dmat4.Rotate(-angle, dvec3.UnitY);

            var modelRotation = dmat4.Rotate(glm.Radians(90.0), dvec3.UnitX);

            ModelMatrix = translation * rotation * modelRotation;
            Position = new dvec3(translation.Column3);
        }
    }
}
