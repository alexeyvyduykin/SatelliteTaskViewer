using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using SatelliteTaskViewer.ViewModels.Entities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public class TaskListEditorViewModel : ViewModelBase
    {      
        private readonly ReadOnlyObservableCollection<SatelliteTask> _tasks;
        private readonly IObservable<Func<BaseSatelliteEvent, bool>> _dynamicFilter;

        public TaskListEditorViewModel(IList<(string Name, Satellite satellite, IList<BaseSatelliteEvent> Events)> tasks)
        {
            IsRotation = true;
            IsObservation = true;
            IsTransmission = true;
            SearchString = string.Empty;

            _dynamicFilter = this.WhenAnyValue(s => s.IsRotation, s => s.IsObservation, s => s.IsTransmission, s => s.SearchString).
                Select(CreatePredicate);

            var source = new SourceCache<SatelliteTask, string>(task => task.Name);

            source.Edit(innerCache =>
            {
                innerCache.Clear();
                innerCache.AddOrUpdate(tasks.Select(s => new SatelliteTask(s.Name, s.satellite, s.Events, _dynamicFilter)));
            });

            var cancellation = source
              .Connect()
              .Bind(out _tasks)
              .DisposeMany()
              .Subscribe();
                   
            Tasks.ToObservableChangeSet().
                AutoRefresh(task => task.IsVisible).
                Subscribe(changeSet =>
                {
                    var task = changeSet.Single().Item.Current;

                    if (task == null)
                    {
                        return;
                    }

                    if (task.IsVisible == true)
                    {
                        CurrentTask = task;
                    }
                });

            Tasks.ToObservableChangeSet().
                AutoRefresh(task => task.SelectedEvent).
                Subscribe(changeSet =>
            {
                var task = changeSet.Single().Item.Current;

                if (task == null || task.SelectedEvent == null)
                {
                    return;
                }

                //if (_scenario.SceneTimerEditor.Timer.IsRunning == true)
                //{
                //    _scenario.SceneTimerEditor.OnPause();
                //}

                //var time = selectedEvent.Epoch.AddSeconds(selectedEvent.BeginTime);
                //var begin = _scenario.SceneTimerEditor.Begin;

                //_scenario.SceneTimerEditor.Update((time - begin).TotalSeconds);
            });
        }

        public ReadOnlyObservableCollection<SatelliteTask> Tasks => _tasks;

        [Reactive]
        public bool IsRotation { get; set; }

        [Reactive]
        public bool IsObservation { get; set; }

        [Reactive]
        public bool IsTransmission { get; set; }

        [Reactive]
        public string SearchString { get; set; }
         
        [Reactive]
        public SatelliteTask? CurrentTask { get; set; }

        private static Func<BaseSatelliteEvent, bool> CreatePredicate((bool, bool, bool, string) arg)
        {
            Func<BaseSatelliteEvent, bool> rotationPredicate = (s => (arg.Item1 == true) ? s is RotationEvent : false);
            Func<BaseSatelliteEvent, bool> observationPredicate = (s => (arg.Item2 == true) ? s is ObservationEvent : false);
            Func<BaseSatelliteEvent, bool> transmissionPredicate = (s => (arg.Item3 == true) ? s is TransmissionEvent : false);
            Func<BaseSatelliteEvent, bool> namePredicate =
                (s => (string.IsNullOrEmpty(arg.Item4) == false) ? s.Name.Contains(arg.Item4) : true);

            Func<BaseSatelliteEvent, bool> combined =
                s => (rotationPredicate(s) || observationPredicate(s) || transmissionPredicate(s)) && namePredicate(s);

            return combined;
        }
    }
}
