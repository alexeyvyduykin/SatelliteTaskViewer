using Avalonia;
using model = TimelineChart.Core;

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public abstract class XYAxisSeries : ItemsSeries
    {
        public static readonly StyledProperty<string> XAxisKeyProperty = AvaloniaProperty.Register<XYAxisSeries, string>(nameof(XAxisKey), string.Empty);

        public static readonly StyledProperty<string> YAxisKeyProperty = AvaloniaProperty.Register<XYAxisSeries, string>(nameof(YAxisKey), string.Empty);

        static XYAxisSeries()
        {
            XAxisKeyProperty.Changed.AddClassHandler<XYAxisSeries>(AppearanceChanged);
            YAxisKeyProperty.Changed.AddClassHandler<XYAxisSeries>(AppearanceChanged);
        }

        public string XAxisKey
        {
            get
            {
                return GetValue(XAxisKeyProperty);
            }

            set
            {
                SetValue(XAxisKeyProperty, value);
            }
        }

        public string YAxisKey
        {
            get
            {
                return GetValue(YAxisKeyProperty);
            }

            set
            {
                SetValue(YAxisKeyProperty, value);
            }
        }

        protected override void SynchronizeProperties(model.Series series)
        {
            base.SynchronizeProperties(series);
            var s = (model.XYAxisSeries)series;
            s.XAxisKey = XAxisKey;
            s.YAxisKey = YAxisKey;
        }
    }
}
