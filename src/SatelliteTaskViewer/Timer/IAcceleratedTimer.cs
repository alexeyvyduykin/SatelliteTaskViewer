using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Timer
{
    public interface IAcceleratedTimer : ITimer
    {
        void Faster();

        void Slower();
    }
}
