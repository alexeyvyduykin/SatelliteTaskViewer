using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.ViewModels.Data;
using SatelliteTaskViewer.ViewModels.Editors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.ViewModels
{
    public class Configurator : IConfigurator
    {
        private readonly IServiceProvider _serviceProvider;

        public Configurator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Scenario? GetScenario(ScenarioData data)
        {
            var factory = _serviceProvider.GetService<IFactory>();

            var epoch = FromJulianDate(data.JulianDateOnTheDay);
            var begin = epoch.AddSeconds(data.ModelingTimeBegin);
            var duration = TimeSpan.FromSeconds(data.ModelingTimeDuration);

            var scenario = factory.CreateScenarioContainer(data.Name, begin, duration);

            var root = scenario.OutlinerEditor.FrameRoot.First();

            scenario.AddEntity(factory.CreateSpacebox(root));
            scenario.AddEntity(factory.CreateSun(data.Sun, root));
            var earth = scenario.AddEntity(factory.CreateEarth(data.Earth, root));
            var gos = scenario.AddEntity(factory.CreateGroundObjects(data, earth.Frame));
            var gss = scenario.AddEntity(factory.CreateGroundStations(data, earth.Frame));
            var rtrs = scenario.AddEntity(factory.CreateRetranslators(data, root));
            var satellites = scenario.AddEntities(factory.CreateSatellites(data, root, gss, rtrs));

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

            scenario.SceneInfoTab = new SceneInfoTab(scenario.OutlinerEditor);

            return scenario;
        }

        public async Task<Scenario?> GetScenarioFromDatabase()
        {
            try
            {
                return await _serviceProvider.GetService<IDatabaseProvider>().LoadScenarioAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<Scenario?> GetScenarioFromJson()
        {
            try
            {
                return await _serviceProvider.GetService<IJsonDataProvider>().LoadScenarioAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private DateTime FromJulianDate(double jd) => DateTime.FromOADate(jd - 2415018.5);
    }
}
