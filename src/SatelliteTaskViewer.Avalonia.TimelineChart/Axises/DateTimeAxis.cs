using System;
using System.Globalization;
using Avalonia;
using model = TimelineChart.Core;

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public class DateTimeAxis : Axis
    {
        public static readonly StyledProperty<CalendarWeekRule> CalendarWeekRuleProperty = AvaloniaProperty.Register<DateTimeAxis, CalendarWeekRule>(nameof(CalendarWeekRule), CalendarWeekRule.FirstFourDayWeek);

        public static readonly StyledProperty<DateTime> FirstDateTimeProperty = AvaloniaProperty.Register<DateTimeAxis, DateTime>(nameof(FirstDateTime), DateTime.MinValue);

        public static readonly StyledProperty<DayOfWeek> FirstDayOfWeekProperty = AvaloniaProperty.Register<DateTimeAxis, DayOfWeek>(nameof(FirstDayOfWeek), DayOfWeek.Monday);

        public static readonly StyledProperty<model.DateTimeIntervalType> IntervalTypeProperty = AvaloniaProperty.Register<DateTimeAxis, model.DateTimeIntervalType>(nameof(IntervalType), model.DateTimeIntervalType.Auto);

        public static readonly StyledProperty<DateTime> LastDateTimeProperty = AvaloniaProperty.Register<DateTimeAxis, DateTime>(nameof(LastDateTime), DateTime.MaxValue);

        public static readonly StyledProperty<model.DateTimeIntervalType> MinorIntervalTypeProperty = AvaloniaProperty.Register<DateTimeAxis, model.DateTimeIntervalType>(nameof(MinorIntervalType), model.DateTimeIntervalType.Auto);

        static DateTimeAxis()
        {
            PositionProperty.OverrideDefaultValue<DateTimeAxis>(model.AxisPosition.Bottom);
            PositionProperty.Changed.AddClassHandler<DateTimeAxis>(AppearanceChanged);
            CalendarWeekRuleProperty.Changed.AddClassHandler<DateTimeAxis>(DataChanged);
            FirstDayOfWeekProperty.Changed.AddClassHandler<DateTimeAxis>(DataChanged);
            MinorIntervalTypeProperty.Changed.AddClassHandler<DateTimeAxis>(DataChanged);
        }

        public DateTimeAxis()
        {
            InternalAxis = new model.DateTimeAxis();
        }

        public CalendarWeekRule CalendarWeekRule
        {
            get
            {
                return GetValue(CalendarWeekRuleProperty);
            }

            set
            {
                SetValue(CalendarWeekRuleProperty, value);
            }
        }

        public DateTime FirstDateTime
        {
            get
            {
                return GetValue(FirstDateTimeProperty);
            }

            set
            {
                SetValue(FirstDateTimeProperty, value);
            }
        }

        public DayOfWeek FirstDayOfWeek
        {
            get
            {
                return GetValue(FirstDayOfWeekProperty);
            }

            set
            {
                SetValue(FirstDayOfWeekProperty, value);
            }
        }

        public model.DateTimeIntervalType IntervalType
        {
            get
            {
                return GetValue(IntervalTypeProperty);
            }

            set
            {
                SetValue(IntervalTypeProperty, value);
            }
        }

        public DateTime LastDateTime
        {
            get
            {
                return GetValue(LastDateTimeProperty);
            }

            set
            {
                SetValue(LastDateTimeProperty, value);
            }
        }

        public model.DateTimeIntervalType MinorIntervalType
        {
            get
            {
                return GetValue(MinorIntervalTypeProperty);
            }

            set
            {
                SetValue(MinorIntervalTypeProperty, value);
            }
        }

        public override model.Axis CreateModel()
        {
            SynchronizeProperties();
            return InternalAxis;
        }

        protected override void SynchronizeProperties()
        {
            base.SynchronizeProperties();
            var a = (model.DateTimeAxis)InternalAxis;

            a.IntervalType = IntervalType;
            a.MinorIntervalType = MinorIntervalType;
            a.FirstDayOfWeek = FirstDayOfWeek;
            a.CalendarWeekRule = CalendarWeekRule;

            if (FirstDateTime > DateTime.MinValue)
            {
                a.Minimum = model.DateTimeAxis.ToDouble(FirstDateTime);
            }

            if (LastDateTime < DateTime.MaxValue)
            {
                a.Maximum = model.DateTimeAxis.ToDouble(LastDateTime);
            }
        }
    }
}
