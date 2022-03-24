using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using System;
using System.Collections.Generic;

namespace SatelliteTaskViewer.DesignTime
{
    internal class DesignTimeData
    {

        public Scenario GetScenario()
        {
            var begin = DateTime.Now;
            var duration = TimeSpan.FromDays(1);
            var factory = new Factory(null!);
            return factory.CreateScenarioContainer("Scenario1", begin, duration);
        }

        // x y z vx vy vz u   
        //public IList<(double x, double y, double z, double vx, double vy, double vz, double u)> Records { get; set; }

        public static SatelliteData SatelliteData =>
            new SatelliteData("Satellite1",
                    new List<double[]>()
                    {
                        new double[] { 7000.0, -6800.0, 6750.0, 100.0, -150.0, 80.0, 0.0 },
                        new double[] { 7000.0, -6800.0, 6750.0, 100.0, -150.0, 80.0, 1.0 },
                    },
                    0.0, 86400.0, 60.0);

        public static RetranslatorData RetranslatorData =>
                new RetranslatorData("Retranslator1",
                    new List<double[]>()
                    {
                        new double[] { 12000.0, -11500.0, 11750.0, 0.0 },
                        new double[] { 13000.0, -12500.0, 12750.0, 3.0 },
                    },
                    0.0, 86400.0, 60.0);

        public static SensorData SensorData =>
        new SensorData("Satellite1", "SensorData1",
            new List<ShootingRecord>()
            {
                new ShootingRecord(0.0, 10.0, 0.12, 0.24, 550, 600, "GroundObject0043"),
                new ShootingRecord(26.0, 35.0, 0.12, 0.24, 534, 567, "GroundObject0634"),
                new ShootingRecord(56.0, 67.0, -0.22, -0.24, 554, 577, "GroundObject0234"),
            },
            0.0, 86400.0);

        public static AntennaData AntennaData =>
            new AntennaData("Satellite1", "AntennaData1",
                new List<TranslationRecord>()
                {
                    new TranslationRecord(0.0, 10.0, "RTR0000001"),
                    new TranslationRecord(17.0, 23.0, "RTR0000002"),
                    new TranslationRecord(56.0, 60.0, "GST0000002")
                },
                0.0, 86400.0);

        public static RotationData RotationData =>
            new RotationData("Satellite1", "RotationData1",
                new List<RotationRecord>()
                {
                    new RotationRecord(0.0, 10.0, 25.0),
                    new RotationRecord(12.0, 15.0, -25.0),
                    new RotationRecord(22.0, 30.0, 25.0)
                },
                0.0, 86400.0);

        public static SunData SunData =>
            new SunData("Sun",
                new GlmSharp.dvec3(40000.0, -42000.0, 41500.0),
                new GlmSharp.dvec3(38000.0, -44000.0, 37500.0),
                0.0, 86400.0);
    }
}
