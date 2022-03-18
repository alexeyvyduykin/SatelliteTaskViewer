using SatelliteTaskViewer.ViewModels.Containers;
using ReactiveUI;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public class PropertiesEditorViewModel : ViewModelBase
    {
        private readonly ScenarioContainerViewModel _scenario;

        public PropertiesEditorViewModel(ScenarioContainerViewModel scenario)
        {
            _scenario = scenario;
        }
    }
}
