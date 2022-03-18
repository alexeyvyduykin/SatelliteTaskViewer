using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IPresenterContract
    {
        void ReadPixels(IntPtr pixels, int rowBytes);
        
        void DrawBegin();

        void Resize(double width, double height);

        int Width { get; }

        int Height { get; }
    }

}
