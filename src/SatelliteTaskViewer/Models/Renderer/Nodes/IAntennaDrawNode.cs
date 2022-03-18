using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IAntennaDrawNode : IDrawNode
    {
        AntennaRenderModel Antenna { get; }
    }
}
