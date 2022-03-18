using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IDataProvider
    {
        Task<ProjectContainerViewModel?> LoadProject();
        Task<ScenarioData?> LoadData();
    }
}
