using SatelliteTaskViewer.FileSystem;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Avalonia.DataProvider.Json
{
    public class JsonDataProvider : IJsonDataProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IFileSystem _fileSystem;
        private readonly IConfigurator _configurator;
        private readonly SolutionFolder _datafolder;

        public JsonDataProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = serviceProvider.GetService<IJsonSerializer>();
            _fileSystem = serviceProvider.GetService<IFileSystem>();
            _configurator = serviceProvider.GetService<IConfigurator>();

            _datafolder = new SolutionFolder("data");
        }

        public async Task<Scenario?> LoadScenarioAsync()
        {
            var data = await LoadDataAsync();

            if (data != null)
            {
                return _configurator.GetScenario(data);
            }

            return default;
        }

        public async Task<ScenarioData?> LoadDataAsync()
        {
            var path = _datafolder.GetPaths("*.json").FirstOrDefault();

            if (path == default)
            {
                throw new Exception($"Not found scenario path={path}");
            }

            return await Task.Run(() => CreateDataFromPath<ScenarioData>(path));
        }

        private T? CreateDataFromPath<T>(string path)
        {
            var json = _fileSystem.ReadUtf8Text(path);

            return CreateDataFromJson<T>(json);
        }

        private T? CreateDataFromJson<T>(string json)
        {
            try
            {
                return _jsonSerializer.Deserialize<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
