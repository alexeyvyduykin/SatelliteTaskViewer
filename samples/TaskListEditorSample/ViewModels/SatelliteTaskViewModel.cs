using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace TaskListEditorSample.ViewModels
{
    public class SatelliteTaskViewModel : ReactiveObject
    {
        private DateTime TimeOrigin { get; } = new DateTime(1899, 12, 31, 0, 0, 0, DateTimeKind.Utc);

        private readonly Models.SatelliteTask _task;
        private readonly ReadOnlyObservableCollection<BaseSatelliteEvent> _events;

        private readonly ReadOnlyObservableCollection<Rotation> _rotations;
        private readonly ReadOnlyObservableCollection<Observation> _observations;
        private readonly ReadOnlyObservableCollection<Transmission> _transmissions;

        private readonly SourceCache<BaseSatelliteEvent, string> _source;

        public ReadOnlyObservableCollection<BaseSatelliteEvent> Events => _events;
        public ReadOnlyObservableCollection<Rotation> Rotations => _rotations;
        public ReadOnlyObservableCollection<Observation> Observations => _observations;
        public ReadOnlyObservableCollection<Transmission> Transmissions => _transmissions;

        public SatelliteTaskViewModel(Models.SatelliteTask task, IObservable<Func<BaseSatelliteEvent, bool>> filter)
        {
            _task = task;
            _source = new SourceCache<BaseSatelliteEvent, string>(e => e.Name);

            _source.Edit(innerCache =>
            {
                innerCache.Clear();
                innerCache.AddOrUpdate(task.Events);
            });

            var cancellation = _source.
                Connect().
                Filter(filter).
                Sort(SortExpressionComparer<BaseSatelliteEvent>.Descending(t => t.BeginTime), SortOptimisations.ComparesImmutableValuesOnly, 25).
                Bind(out _events).
                DisposeMany().
                Subscribe();

            var cancellation1 = _source.
                Connect().
                Filter(s => s is RotationEvent).
                Filter(filter).
                Transform(s => new Rotation()
                {
                    BeginTime = s.Begin,
                    EndTime = s.Begin + s.Duration
                }).
                Sort(SortExpressionComparer<Rotation>.Descending(t => t.BeginTime), SortOptimisations.ComparesImmutableValuesOnly, 25).
                Bind(out _rotations).
                DisposeMany().
                Subscribe();

            var cancellation2 = _source.
                Connect().
                Filter(s => s is ObservationEvent).
                Filter(filter).
                Transform(s => new Observation()
                {
                    BeginTime = s.Begin,
                    EndTime = s.Begin + s.Duration
                }).
                Sort(SortExpressionComparer<Observation>.Descending(t => t.BeginTime), SortOptimisations.ComparesImmutableValuesOnly, 25).
                Bind(out _observations).
                DisposeMany().
                Subscribe();

            var cancellation3 = _source.
                Connect().
                Filter(s => s is TransmissionEvent).
                Filter(filter).
                Transform(s => new Transmission()
                {
                    BeginTime = s.Begin,
                    EndTime = s.Begin + s.Duration
                }).
                Sort(SortExpressionComparer<Transmission>.Descending(t => t.BeginTime), SortOptimisations.ComparesImmutableValuesOnly, 25).
                Bind(out _transmissions).
                DisposeMany().
                Subscribe();


            SelectedEvent = Events.FirstOrDefault();
            Epoch = task.Events.First().Epoch;

            BeginScenario = ToTotalDays(Epoch.Date, TimeOrigin);
            EndScenario = BeginScenario + 2;

            Begin = ToTotalDays(Epoch, TimeOrigin);
            Duration = 1.0;

            Labels = new ObservableCollection<LabelItem>()
            {
                new() { Label = "Rotation" },
                new() { Label = "Observation" },
                new() { Label = "Transmission" }
            };
        }

        [Reactive]
        public ObservableCollection<LabelItem> Labels { get; set; }

        public string Name => _task.Name;

        [Reactive]
        public double BeginScenario { get; set; }

        [Reactive]
        public double EndScenario { get; set; }

        [Reactive]
        public double Begin { get; set; }

        [Reactive]
        public double Duration { get; set; }

        [Reactive]
        public DateTime Epoch { get; set; }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public double CurrentTime { get; set; }

        [Reactive]
        public BaseSatelliteEvent? SelectedEvent { get; set; }

        public static double ToTotalDays(DateTime value, DateTime timeOrigin)
        {
            return (value - timeOrigin).TotalDays + 1;
        }
    }

    public class LabelItem
    {
        public string Label { get; set; }
    }

    public class Rotation
    {
        public string Category => "Rotation";

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class Observation
    {
        public string Category => "Observation";

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class Transmission
    {
        public string Category => "Transmission";

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
