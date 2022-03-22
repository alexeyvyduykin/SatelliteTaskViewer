using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IDataProvider
    {
        Task<Scenario?> LoadScenarioAsync();

        Task<ScenarioData?> LoadDataAsync();
    }
}
