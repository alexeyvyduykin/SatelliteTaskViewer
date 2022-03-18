using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteTaskViewer;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IVertexArray : IDisposable
    {
        void Bind();

        void Clean();

        int MaximumArrayIndex();

        IVertexBufferAttributes Attributes { get; set; }

        IIndexBuffer IndexBuffer { get; set; }

        bool DisposeBuffers { get; set; }
    }

}
