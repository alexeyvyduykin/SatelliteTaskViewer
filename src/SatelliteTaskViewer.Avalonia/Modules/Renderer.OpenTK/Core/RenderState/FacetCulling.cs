using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;


namespace SatelliteTaskViewer.Avalonia.Renderer.OpenTK.Core
{
    internal class FacetCulling
    {
        public FacetCulling()
        {            
            Enabled = true;
            Face = CullFaceMode.Back;
            FrontFaceWindingOrder = FrontFaceDirection.Ccw;
        }

        public bool Enabled { get; set; }
        public CullFaceMode Face { get; set; }
        public FrontFaceDirection FrontFaceWindingOrder { get; set; }
    }
}
