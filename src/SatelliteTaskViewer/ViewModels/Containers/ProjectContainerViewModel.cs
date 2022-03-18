using System.Collections.Immutable;
using SatelliteTaskViewer.ViewModels.Editors;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Containers
{
    public partial class ProjectContainerViewModel : BaseContainerViewModel
    {
        public ProjectContainerViewModel()
        {
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Selected))
                {
                    if (Selected != null)
                    {
                        SetSelected(Selected);
                    }
                }
            };
        }

        [Reactive]
        public ImmutableArray<ScenarioContainerViewModel> Scenarios { get; set; }

        [Reactive]
        public ScenarioContainerViewModel? CurrentScenario { get; set; }

        [Reactive]
        public ViewModelBase? Selected { get; set; }

        [Reactive]
        public TopBarViewModel? TopBar { get; set; }

        public void SetCurrentScenario(ScenarioContainerViewModel scenario)
        {
            CurrentScenario = scenario;
            Selected = scenario;
        }

        public void SetSelected(ViewModelBase value)
        {
            if (value is ScenarioContainerViewModel scenario)
            {
                CurrentScenario = scenario;
            }
        }
    }
}
