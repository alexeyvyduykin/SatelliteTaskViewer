using System;
using System.Collections.Generic;
using System.Text;
using GlmSharp;

namespace SatelliteTaskViewer.Models
{
    public interface IFrameable
    {
        dmat4 ModelMatrix { get; }
    }
}
