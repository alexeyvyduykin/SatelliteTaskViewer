using System;
using GlmSharp;
using SatelliteTaskViewer.Models;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class GroundObjectState : BaseState, IFrameable
    {
        private readonly double _earthRadius;

        public GroundObjectState(GroundObjectData data)
        {
            Lon = data.Lon;
            Lat = data.Lat;
            _earthRadius = data.EarthRadius;

            Update();
        }

        [Reactive]
        public dvec3 Position { get; protected set; }

        [Reactive]
        public double Lon { get; protected set; }

        [Reactive]
        public double Lat { get; protected set; }

        private void Update()
        {
            double lon = glm.Radians(Lon);
            double lat = glm.Radians(Lat);
            double r = _earthRadius;

            var model3x3 = new dmat3();
            model3x3.m02 = -Math.Cos(lon) * Math.Sin(lat);
            model3x3.m12 = Math.Cos(lon) * Math.Cos(lat);
            model3x3.m22 = -Math.Sin(lon);
            model3x3.m00 = -Math.Sin(lon) * Math.Sin(lat);
            model3x3.m10 = Math.Sin(lon) * Math.Cos(lat);
            model3x3.m20 = Math.Cos(lon);
            model3x3.m01 = Math.Cos(lat);
            model3x3.m11 = Math.Sin(lat);
            model3x3.m21 = 0.0;

            ModelMatrix = new dmat4(model3x3) * dmat4.Translate(new dvec3(0.0, r, 0.0));
            Position = new dvec3(ModelMatrix.Column3);
        }
    }
}
