using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IOrbitDrawNode : IDrawNode
    {
        OrbitRenderModel Orbit { get; }
    }
}
