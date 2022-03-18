using System.Collections.Generic;
using System.Linq;
using GlmSharp;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class OrbitState : BaseState
    {
        public OrbitState(OrbitData data)
        {
            Vertices = data.Records.Select(s => (s[0], s[1], s[2], s[3])).ToList();

            ModelMatrix = dmat4.Identity;
        }

        [Reactive]
        public IList<(double x, double y, double z, double u)> Vertices { get; protected set; }
    }
}
