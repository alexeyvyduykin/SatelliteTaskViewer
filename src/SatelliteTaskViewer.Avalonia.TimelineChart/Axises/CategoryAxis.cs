using System.Collections;
using System.Collections.Generic;
using Avalonia;
using model = TimelineChart.Core;
#nullable disable

namespace SatelliteTaskViewer.Avalonia.TimelineChart
{
    public class CategoryAxis : LinearAxis
    {
        public static readonly StyledProperty<double> GapWidthProperty = AvaloniaProperty.Register<CategoryAxis, double>(nameof(GapWidth), 1.0);

        public static readonly StyledProperty<bool> IsTickCenteredProperty = AvaloniaProperty.Register<CategoryAxis, bool>(nameof(IsTickCentered), false);

        public static readonly StyledProperty<IEnumerable> ItemsProperty = AvaloniaProperty.Register<CategoryAxis, IEnumerable>(nameof(Items), null);

        public static readonly StyledProperty<string> LabelFieldProperty = AvaloniaProperty.Register<CategoryAxis, string>(nameof(LabelField), null);

        public static readonly StyledProperty<IList<string>> LabelsProperty = AvaloniaProperty.Register<CategoryAxis, IList<string>>(nameof(Labels), new List<string>());

        static CategoryAxis()
        {
            PositionProperty.OverrideMetadata(typeof(CategoryAxis), new StyledPropertyMetadata<model.AxisPosition>(model.AxisPosition.Bottom));
            MajorStepProperty.OverrideMetadata(typeof(CategoryAxis), new StyledPropertyMetadata<double>(1.0));
            IsTickCenteredProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
            ItemsProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
            LabelFieldProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
            LabelsProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
            PositionProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
            MajorStepProperty.Changed.AddClassHandler<CategoryAxis>(DataChanged);
        }

        public CategoryAxis()
        {
            InternalAxis = new model.CategoryAxis();
        }

        public double GapWidth
        {
            get
            {
                return GetValue(GapWidthProperty);
            }

            set
            {
                SetValue(GapWidthProperty, value);
            }
        }

        public bool IsTickCentered
        {
            get
            {
                return GetValue(IsTickCenteredProperty);
            }

            set
            {
                SetValue(IsTickCenteredProperty, value);
            }
        }

        public IEnumerable Items
        {
            get
            {
                return GetValue(ItemsProperty);
            }

            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public string LabelField
        {
            get
            {
                return GetValue(LabelFieldProperty);
            }

            set
            {
                SetValue(LabelFieldProperty, value);
            }
        }

        public IList<string> Labels
        {
            get
            {
                return GetValue(LabelsProperty);
            }

            set
            {
                SetValue(LabelsProperty, value);
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
            var a = (model.CategoryAxis)InternalAxis;
            a.IsTickCentered = IsTickCentered;
            a.ItemsSource = Items;
            a.LabelField = LabelField;
            a.GapWidth = GapWidth;

            if (Labels != null && Items == null)
            {
                a.Labels.Clear();
                a.Labels.AddRange(Labels);
            }
        }
    }
}
