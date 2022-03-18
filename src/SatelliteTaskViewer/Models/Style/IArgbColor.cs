using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Models.Style
{
    public interface IArgbColor : IColor
    {
        byte A { get; set; }

        byte R { get; set; }

        byte G { get; set; }

        byte B { get; set; }
    }
}
