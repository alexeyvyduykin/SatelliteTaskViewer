using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models
{
    public interface IConfigurator
    {
        Scenario? GetScenario(ScenarioData data);

        Task<Scenario?> GetScenarioFromDatabase();

        Task<Scenario?> GetScenarioFromJson();
    }
}
