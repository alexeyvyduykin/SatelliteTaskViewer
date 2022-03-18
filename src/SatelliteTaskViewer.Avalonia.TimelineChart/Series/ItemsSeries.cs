using model = TimelineChart.Core;

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public abstract class ItemsSeries : Series
    {
        protected override void SynchronizeProperties(model.Series series)
        {
            base.SynchronizeProperties(series);
            var s = (model.ItemsSeries)series;
            s.ItemsSource = Items;
        }
    }
}
