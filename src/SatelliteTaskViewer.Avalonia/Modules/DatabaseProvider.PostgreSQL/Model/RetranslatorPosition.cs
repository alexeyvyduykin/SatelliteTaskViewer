﻿#nullable disable

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    internal partial class RetranslatorPosition
    {
        public int Id { get; set; }
        public double PositionTime { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
        public double TrueAnomaly { get; set; }
        public int RetranslatorId { get; set; }

        public virtual Retranslator Retranslator { get; set; }
    }
}
