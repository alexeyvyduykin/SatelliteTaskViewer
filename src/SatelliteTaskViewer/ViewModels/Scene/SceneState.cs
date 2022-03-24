using System;
using System.Collections.Generic;
using GlmSharp;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Scene;
using ReactiveUI.Fody.Helpers;
#nullable disable

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class SceneState : ViewModelBase, ISceneState
    {
        public SceneState() { }

        public dmat4 ViewMatrix
        {
            get
            {
                if (Camera == null || Target == null)
                {
                    return dmat4.AllNaN;
                }

                return Camera.ViewMatrix * Target.InverseAbsoluteModel;
            }
        }

        public dmat4 ProjectionMatrix => dmat4.Perspective(FieldOfViewY, AspectRatio, PerspectiveNearPlaneDistance, PerspectiveFarPlaneDistance);

        [Reactive]
        public ICamera Camera { get; set; }

        [Reactive]
        public ITargetable Target { get; set; }

        [Reactive]
        public IDictionary<Type, (dvec3 eye, Func<double, double> func)> CameraBehaviours { get; set; }

        [Reactive]
        public dvec4 LightPosition { get; set; }

        [Reactive]
        public float DiffuseIntensity { get; set; }

        [Reactive]
        public float SpecularIntensity { get; set; }

        [Reactive]
        public float AmbientIntensity { get; set; }

        [Reactive]
        public float Shininess { get; set; }

        [Reactive]
        public float HighResolutionSnapScale { get; set; }

        // FOV
        [Reactive]
        public double FieldOfViewX { get; set; }

        [Reactive]
        public double FieldOfViewY { get; set; }

        [Reactive]
        public double AspectRatio { get; set; }

        [Reactive]
        public double PerspectiveNearPlaneDistance { get; set; }

        [Reactive]
        public double PerspectiveFarPlaneDistance { get; set; }
    }
}
