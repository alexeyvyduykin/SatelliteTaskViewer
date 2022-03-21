using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels.Containers;
using SatelliteTaskViewer.ViewModels.Data;
using System;

namespace SatelliteTaskViewer.Models.Editor
{
    public interface IContainerFactory
    {
        //ProjectContainerViewModel GetProject();

        ProjectContainerViewModel? GetProject(ScenarioData data);

        Task<ProjectContainerViewModel?> GetFromDatabase();

        Task<ProjectContainerViewModel?> GetFromJson();

        Task SaveFromDatabaseToJson();

        ProjectContainerViewModel GetEmptyProject();
    }
}
