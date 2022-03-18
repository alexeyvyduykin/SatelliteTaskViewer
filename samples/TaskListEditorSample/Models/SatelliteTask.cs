using SatelliteTaskViewer.ViewModels.Entities;
using System.Collections.Generic;

namespace TaskListEditorSample.Models
{
    public class SatelliteTask
    {
        private readonly IList<BaseSatelliteEvent> _events;
        private readonly string _name;

        public SatelliteTask(string name, IList<BaseSatelliteEvent> events)
        {
            _name = name;
            _events = events;
        }

        public IList<BaseSatelliteEvent> Events => _events;

        public string Name => _name;
    }
}
