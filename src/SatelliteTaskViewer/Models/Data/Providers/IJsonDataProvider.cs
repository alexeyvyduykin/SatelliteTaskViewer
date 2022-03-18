using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.ViewModels.Data;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IJsonDataProvider : IDataProvider
    {
        T? CreateDataFromJson<T>(string json);

        T? CreateDataFromPath<T>(string path);

        void Save(ScenarioData data);
    }

}
