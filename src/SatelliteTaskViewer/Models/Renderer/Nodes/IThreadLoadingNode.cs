using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.Models.Image;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IThreadLoadingNode
    {
        bool IsComplete { get; }
        string WaitKey { get; }
        int SetImage(IDdsImage image);
        void SetName(int name);     
    }
}
