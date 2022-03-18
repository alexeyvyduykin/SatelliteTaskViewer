using System;
using System.Collections.Generic;

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    internal partial class GroundObject
    {
        public GroundObject()
        {
            SatelliteShootings = new HashSet<SatelliteShooting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }

        public virtual ICollection<SatelliteShooting> SatelliteShootings { get; set; }
    }
}
