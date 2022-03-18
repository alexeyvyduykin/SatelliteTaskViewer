using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface ICleanableObserver
    {
        void NotifyDirty(ICleanable value);
    }
}
