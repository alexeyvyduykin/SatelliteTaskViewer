using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IShaderVertexAttribute
    {
        string Name { get; }

        int Location { get; }

        ActiveAttribType Datatype { get; }

        int Length { get; }
    }
}
