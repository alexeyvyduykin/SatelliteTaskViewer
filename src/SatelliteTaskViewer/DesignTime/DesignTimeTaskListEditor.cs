using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Editors;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeTaskListEditor : TaskListEditorViewModel
    {
        public DesignTimeTaskListEditor() : base(CreateSatelliteTasks())
        {

        }

        public static IList<(string Name, Satellite Satellite, IList<BaseSatelliteEvent> Events)> CreateSatelliteTasks()
        {
            var designTimeData = new DesignTimeData();

            var satellite = new Satellite() { Name = DesignTimeData.SatelliteData.Name };

            var list = new List<(string, Satellite, IList<BaseSatelliteEvent>)>();

            var epochOnDay = DateTime.Now;

            var name = satellite.Name;

            var events = new List<BaseSatelliteEvent>();

            events.AddRange(CreateRotationEvents(DesignTimeData.RotationData, epochOnDay));
            events.AddRange(CreateObservationEvents(DesignTimeData.SensorData, epochOnDay));
            events.AddRange(CreateTransmissionEvents(DesignTimeData.AntennaData, epochOnDay));

            events = events.OrderBy(s => s.BeginTime).ToList();

            list.Add((name, satellite, events));

            return list;

            IEnumerable<BaseSatelliteEvent> CreateRotationEvents(RotationData data, DateTime epochOnDay)
            {
                var events = new List<BaseSatelliteEvent>();

                int count = 0;

                var dt = epochOnDay.AddSeconds(data.TimeBegin);
                foreach (var item in data.Rotations)
                {
                    events.Add(new RotationEvent()
                    {
                        Name = $"Rotation{++count:000}: " + ((item.Angle < 0) ? "Left" : "Right"),
                        Epoch = dt,
                        BeginTime = item.BeginTime,
                        EndTime = item.EndTime,
                        //Begin = dt.AddSeconds(item.BeginTime),
                        //Duration = TimeSpan.FromSeconds(item.EndTime - item.BeginTime),
                    });
                }

                return events;
            }

            IEnumerable<BaseSatelliteEvent> CreateObservationEvents(SensorData data, DateTime epochOnDay)
            {
                var events = new List<BaseSatelliteEvent>();
                int count = 0;
                var dt = epochOnDay.AddSeconds(data.TimeBegin);
                foreach (var item in data.Shootings)
                {
                    events.Add(new ObservationEvent()
                    {
                        Name = $"Observation{++count:000}: " + item.TargetName,
                        Epoch = dt,
                        BeginTime = item.BeginTime,
                        EndTime = item.EndTime,
                        //Begin = dt.AddSeconds(item.BeginTime),
                        //Duration = TimeSpan.FromSeconds(item.EndTime - item.BeginTime),
                    });
                }

                return events;
            }

            IEnumerable<BaseSatelliteEvent> CreateTransmissionEvents(AntennaData data, DateTime epochOnDay)
            {
                var events = new List<BaseSatelliteEvent>();
                int count = 0;
                var dt = epochOnDay.AddSeconds(data.TimeBegin);
                foreach (var item in data.Translations)
                {
                    events.Add(new TransmissionEvent()
                    {
                        Name = $"Transmission{++count:000}: " + item.Target,
                        Epoch = dt,
                        BeginTime = item.BeginTime,
                        EndTime = item.EndTime,
                        //Begin = dt.AddSeconds(item.BeginTime),
                        //Duration = TimeSpan.FromSeconds(item.EndTime - item.BeginTime),
                    });
                }
                return events;
            }
        }
    }
}
