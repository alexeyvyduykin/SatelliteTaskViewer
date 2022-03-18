using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Input;
using Avalonia.Layout;
using Spatial;
using model = TimelineChart.Core;

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public static class Extensions
    {
        public static bool IsEmpty(this OxyRect rect)
        {
            return rect.Left == 0.0 && rect.Right == 0.0 && rect.Width == 0.0 && rect.Height == 0.0;
        }

        public static bool IsEmpty(this ScreenPoint point)
        {
            return point.X == 0.0 && point.Y == 0.0;
        }

        public static void AddRange<T>(this ObservableCollection<T> arr, IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                arr.Add(item);
            }
        }

        public static model.OxyMouseWheelEventArgs ToMouseWheelEventArgs(this PointerWheelEventArgs e, IInputElement relativeTo)
        {
            return new model.OxyMouseWheelEventArgs
            {
                Position = e.GetPosition(relativeTo).ToScreenPoint(),
                Delta = (int)(e.Delta.Y + e.Delta.X) * 120
            };
        }

        public static ScreenPoint ToScreenPoint(this Point pt)
        {
            return new ScreenPoint(pt.X, pt.Y);
        }

        public static model.OxyMouseDownEventArgs ToMouseDownEventArgs(this PointerPressedEventArgs e, IInputElement relativeTo)
        {
            var point = e.GetCurrentPoint(relativeTo);

            return new model.OxyMouseDownEventArgs
            {
                ChangedButton = point.Properties.PointerUpdateKind.Convert(),
                Position = e.GetPosition(relativeTo).ToScreenPoint(),
            };
        }
        public static model.OxyMouseEventArgs ToMouseReleasedEventArgs(this PointerReleasedEventArgs e, IInputElement relativeTo)
        {
            return new model.OxyMouseEventArgs
            {
                Position = e.GetPosition(relativeTo).ToScreenPoint(),
            };
        }

        public static model.OxyMouseEventArgs ToMouseEventArgs(this PointerEventArgs e, IInputElement relativeTo)
        {
            return new model.OxyMouseEventArgs
            {
                Position = e.GetPosition(relativeTo).ToScreenPoint(),
            };
        }

        public static model.OxyMouseButton Convert(this PointerUpdateKind pointerUpdateKind)
        {
            return pointerUpdateKind switch
            {
                PointerUpdateKind.LeftButtonPressed => model.OxyMouseButton.Left,
                PointerUpdateKind.MiddleButtonPressed => model.OxyMouseButton.Middle,
                PointerUpdateKind.RightButtonPressed => model.OxyMouseButton.Right,
                _ => model.OxyMouseButton.None,
            };
        }

        public static HorizontalAlignment ToAvalonia(this model.HorizontalAlignment horizontal)
        {
            return horizontal switch
            {
                model.HorizontalAlignment.Left => HorizontalAlignment.Left,
                model.HorizontalAlignment.Center => HorizontalAlignment.Center,
                model.HorizontalAlignment.Right => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Stretch,
            };
        }

        public static VerticalAlignment ToAvalonia(this model.VerticalAlignment vertical)
        {
            return vertical switch
            {
                model.VerticalAlignment.Bottom => VerticalAlignment.Bottom,
                model.VerticalAlignment.Middle => VerticalAlignment.Center,
                model.VerticalAlignment.Top => VerticalAlignment.Top,
                _ => VerticalAlignment.Stretch,
            };
        }
    }
}
