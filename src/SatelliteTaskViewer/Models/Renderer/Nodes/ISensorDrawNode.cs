using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.ViewModels.Scene;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface ISensorDrawNode : IDrawNode
    {
        SensorRenderModel Sensor { get; }
    }
}
