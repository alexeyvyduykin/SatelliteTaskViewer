using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Editor
{
    public interface IContainerFactory
    {
        Scenario? GetScenario(ScenarioData data);

        Task<Scenario?> GetFromDatabase();

        Task<Scenario?> GetFromJson();

        Task SaveFromDatabaseToJson();

        Scenario GetEmptyScenario();
    }
}
