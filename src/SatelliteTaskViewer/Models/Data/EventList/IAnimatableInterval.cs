﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Data
{
    internal interface IAnimatableInterval<T> where T : IEventState
    {
        T Animate(double t);
    }
}
