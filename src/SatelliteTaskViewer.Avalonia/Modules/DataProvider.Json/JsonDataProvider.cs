using System;
using System.IO;
using System.Threading.Tasks;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using Microsoft.Extensions.Configuration;

namespace SatelliteTaskViewer.Avalonia.DataProvider.Json
{
    public class JsonDataProvider : IJsonDataProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IFileSystem _fileSystem;

        public JsonDataProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _jsonSerializer = serviceProvider.GetService<IJsonSerializer>();
            _fileSystem = _serviceProvider.GetService<IFileSystem>();
        }

        public T? CreateDataFromJson<T>(string json)
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

        public T? CreateDataFromPath<T>(string path)
        {
            var json = _fileSystem.ReadUtf8Text(path);

            return CreateDataFromJson<T>(json);
        }

        public void Save(ScenarioData data)
        {
            var fileIO = _serviceProvider.GetService<IFileSystem>();

            var configuration = _serviceProvider.GetService<IConfigurationRoot>();

            var dataPath = configuration["DataPath"];
            var projectFilename = configuration["ProjectFilename"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), dataPath);

            var json = _jsonSerializer.Serialize<ScenarioData>(data);

            fileIO.WriteUtf8Text(Path.Combine(path, projectFilename), json);
        }

        public async Task<Scenario?> LoadScenario()
        {
            var data = await LoadData();
            if (data is not null)
            {
                return _serviceProvider.GetService<IContainerFactory>().GetScenario(data);
            }

            return default;
        }

        public async Task<ScenarioData?> LoadData()
        {
            var configuration = _serviceProvider.GetService<IConfigurationRoot>();

            var dataPath = configuration["DataPath"];
            var projectFilename = configuration["ProjectFilename"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), dataPath);

            return await Task.Run(() => CreateDataFromPath<ScenarioData>(Path.Combine(path, projectFilename)));
        }
    }
}
