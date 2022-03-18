﻿using System;
using System.Collections.Generic;

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    internal partial class SatelliteRotation
    {
        public int Id { get; set; }
        public double Begin { get; set; }
        public double Duration { get; set; }
        public double FromAngle { get; set; }
        public double ToAngle { get; set; }
        public int SatelliteId { get; set; }

        public virtual Satellite Satellite { get; set; }
    }
}
