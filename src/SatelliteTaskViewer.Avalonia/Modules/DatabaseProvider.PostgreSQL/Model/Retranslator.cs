using System.Collections.Generic;
#nullable disable

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    internal partial class Retranslator
    {
        public Retranslator()
        {
            RetranslatorPositions = new HashSet<RetranslatorPosition>();
            SatelliteToRetranslatorTransfers = new HashSet<SatelliteToRetranslatorTransfer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double JulianDateOnTheDay { get; set; }
        public double LifetimeBegin { get; set; }
        public double LifetimeDuration { get; set; }

        public virtual ICollection<RetranslatorPosition> RetranslatorPositions { get; set; }
        public virtual ICollection<SatelliteToRetranslatorTransfer> SatelliteToRetranslatorTransfers { get; set; }
    }
}
