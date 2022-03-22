using System;
using System.IO;
using System.Threading.Tasks;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using Microsoft.Extensions.Configuration;

namespace SatelliteTaskViewer.Avalonia.DataProvider.Json
{
    public class JsonDataProvider : IJsonDataProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IFileSystem _fileSystem;
        private readonly IConfigurator _configurator;
        private readonly IConfigurationRoot _configurationRoot;

        public JsonDataProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = serviceProvider.GetService<IJsonSerializer>();
            _fileSystem = serviceProvider.GetService<IFileSystem>();
            _configurator = serviceProvider.GetService<IConfigurator>();
            _configurationRoot = serviceProvider.GetService<IConfigurationRoot>();
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
            var dataPath = _configurationRoot["DataPath"];
            var projectFilename = _configurationRoot["ProjectFilename"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), dataPath);

            return await Task.Run(() => CreateDataFromPath<ScenarioData>(Path.Combine(path, projectFilename)));
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
