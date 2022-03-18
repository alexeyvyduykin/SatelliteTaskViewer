using model = TimelineChart.Core;

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public class LinearAxis : Axis
    {
        public LinearAxis()
        {
            InternalAxis = new model.LinearAxis();
        }

        public override model.Axis CreateModel()
        {
            SynchronizeProperties();
            return InternalAxis;
        }
    }
}
