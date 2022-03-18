using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Avalonia.Renderer.OpenTK.Core
{
    internal interface ICleanableObserver
    {
        void NotifyDirty(ICleanable value);
    }
}
