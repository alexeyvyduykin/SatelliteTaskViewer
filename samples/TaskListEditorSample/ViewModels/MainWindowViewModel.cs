using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaskListEditorSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            TaskListViewModel = new TaskListViewModel(Create());


            this.WhenAnyValue(s => s.TaskListViewModel.CurrentTask).Subscribe(task =>
            {
                if (task == null)
                {
                    return;
                }

                //SetCameraTo(task.Satellite);
            });
        }

        private IList<Models.SatelliteTask> Create()
        {
            var epoch = DateTime.Now;

            Assembly asm = Assembly.GetExecutingAssembly();
            string? path = Path.GetDirectoryName(asm.Location);

            if (string.IsNullOrEmpty(path) == true)
            {
                throw new Exception();
            }

            var path1 = Path.Combine(path, "..\\..\\..\\Data", "satellite1.json");
            var path2 = Path.Combine(path, "..\\..\\..\\Data", "satellite2.json");
            var path3 = Path.Combine(path, "..\\..\\..\\Data", "satellite3.json");
            var path4 = Path.Combine(path, "..\\..\\..\\Data", "satellite4.json");

            var json1 = GetJson(path1);
            var json2 = GetJson(path2);
            var json3 = GetJson(path3);
            var json4 = GetJson(path4);

            var jsons = new List<string>() { json1, json2, json3, json4 };

            List<Models.SatelliteTask> tasks = new();

            int i = 0;
            foreach (var item in jsons)
            {
                var task = CreateSatelliteTaskFromJson($"SatelliteTask{++i}", item, epoch);

                tasks.Add(task);
            }

            return tasks;
        }

        private string GetJson(string path)
        {
            using var fs = File.OpenRead(path);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            return sr.ReadToEnd();
        }

        private Models.SatelliteTask CreateSatelliteTaskFromJson(string name, string json, DateTime epoch)
        {
            var definition = new
            {
                Name = "",
                Rotations = new[] { new { BeginTime = 0.0, EndTime = 0.0 } },
                Observations = new[] { new { BeginTime = 0.0, EndTime = 0.0 } },
                Transmissions = new[] { new { BeginTime = 0.0, EndTime = 0.0 } }
            };

            var obj = JsonConvert.DeserializeAnonymousType(json, definition);

            if (obj == null)
            {
                throw new Exception();
            }

            List<BaseSatelliteEvent> events = new();

            int index = 0;

            foreach (var item in obj.Rotations)
            {
                events.Add(new RotationEvent()
                {
                    BeginTime = item.BeginTime,
                    EndTime = item.EndTime,
                    Epoch = epoch,
                    Name = name + $": Rotation{++index:000}"
                });
            }
            index = 0;
            foreach (var item in obj.Observations)
            {
                events.Add(new ObservationEvent()
                {
                    BeginTime = item.BeginTime,
                    EndTime = item.EndTime,
                    Epoch = epoch,
                    Name = name + $": Observation{++index:000}"
                });
            }
            index = 0;
            foreach (var item in obj.Transmissions)
            {
                events.Add(new TransmissionEvent()
                {
                    BeginTime = item.BeginTime,
                    EndTime = item.EndTime,
                    Epoch = epoch,
                    Name = name + $": Transmission{++index:000}"
                });
            }

            events.Sort((s1, s2) => s1.BeginTime.CompareTo(s2.BeginTime));

            return new Models.SatelliteTask(name, events);
        }

        [Reactive]
        public TaskListViewModel TaskListViewModel { get; set; }
    }
}
