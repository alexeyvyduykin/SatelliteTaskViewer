﻿using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Editors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.ViewModels.Editor
{
    public class ContainerFactory : IContainerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ContainerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //public ProjectContainerViewModel GetProject()
        //{
        //    var factory = _serviceProvider.GetService<IFactory>();      
        //    var project = factory.CreateProjectContainer("Project1");

        //    // Templates
        //    //   var templateBuilder = project.Templates.ToBuilder();
        //    //   templateBuilder.Add(CreateDefaultTemplate(this, project, "Default"));
        //    //   project.Templates = templateBuilder.ToImmutable();

        //    //   project.SetCurrentTemplate(project.Templates.FirstOrDefault(t => t.Name == "Default"));

        //    // Documents and Pages      
        //    var scenario = factory.CreateScenarioContainer("Scenario1", DateTime.Now, TimeSpan.FromDays(1));

        //    var scenarioBuilder = project.Scenarios.ToBuilder();
        //    scenarioBuilder.Add(scenario);
        //    project.Scenarios = scenarioBuilder.ToImmutable();

        //    // project.Selected = scenario.Pages.FirstOrDefault();

        //    return project;
        //}

        public ProjectContainerViewModel? GetProject(ScenarioData data)
        {
            var factory = _serviceProvider.GetService<IFactory>();

            var epoch = FromJulianDate(data.JulianDateOnTheDay);
            var begin = epoch.AddSeconds(data.ModelingTimeBegin);
            var duration = TimeSpan.FromSeconds(data.ModelingTimeDuration);

            var project = factory.CreateProjectContainer("Project1");
            var scenario = factory.CreateScenarioContainer(data.Name, begin, duration);

            project.AddScenario(scenario);
            project.SetCurrentScenario(scenario);

            var root = scenario.OutlinerEditor.FrameRoot.First();

            project.AddEntity(factory.CreateSpacebox(root));
            project.AddEntity(factory.CreateSun(data.Sun, root));
            var earth = project.AddEntity(factory.CreateEarth(data.Earth, root));
            var gos = project.AddEntity(factory.CreateGroundObjects(data, earth.Frame));
            var gss = project.AddEntity(factory.CreateGroundStations(data, earth.Frame));
            var rtrs = project.AddEntity(factory.CreateRetranslators(data, root));
            var satellites = project.AddEntities(factory.CreateSatellites(data, root, gss, rtrs));

            var tasks = factory.CreateSatelliteTasks(satellites, data);
            scenario.TaskListEditor = new TaskListEditorViewModel(tasks);

            scenario.AddGroundObjectList(factory.CreateGroundObjectList(gos));

            var currentTask = scenario.TaskListEditor.CurrentTask;

            if (currentTask == null)
            {
                currentTask = scenario.TaskListEditor.Tasks.FirstOrDefault();
                if (currentTask != null)
                {
                    currentTask.IsVisible = true;
                }
            }
            else
            {
                scenario.SetCameraTo(currentTask.Satellite);
            }

            return project;
        }

        public async Task<ProjectContainerViewModel?> GetFromDatabase()
        {
            try
            {
                return await _serviceProvider.GetService<IDatabaseProvider>().LoadProject();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<ProjectContainerViewModel?> GetFromJson()
        {
            try
            {
                return await _serviceProvider.GetService<IJsonDataProvider>().LoadProject();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task SaveFromDatabaseToJson()
        {
            try
            {
                await _serviceProvider.GetService<IDatabaseProvider>().Save();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private DateTime FromJulianDate(double jd) => DateTime.FromOADate(jd - 2415018.5);

        public ProjectContainerViewModel GetEmptyProject()
        {
            var factory = _serviceProvider.GetService<IFactory>();

            var project = factory.CreateProjectContainer("Project1");
            var scenario1 = factory.CreateScenarioContainer("Scenario1", DateTime.Now, TimeSpan.FromDays(1));

            project.AddScenario(scenario1);
            project.SetCurrentScenario(scenario1);

            return project;
        }
    }
}
