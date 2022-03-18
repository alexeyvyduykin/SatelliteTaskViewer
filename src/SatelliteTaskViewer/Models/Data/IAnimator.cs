using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IAnimator 
    {
        void Animate(double t);
    }
}
