using System;
using System.Linq;
using SatelliteTaskViewer.ViewModels.Containers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public enum Workspace { Layout, Planning };

    public class TopBarViewModel : ViewModelBase
    {
        private readonly ProjectContainerViewModel _project;

        public TopBarViewModel(ProjectContainerViewModel project)
        {
            _project = project;

            this.WhenAnyValue(s => s.ActiveWorkspace).Subscribe(workspace =>
            {
                switch (workspace)
                {
                    case Workspace.Layout:

                        break;
                    case Workspace.Planning:
                    {
                        var scenario = _project.CurrentScenario;

                        var currentTask = scenario.TaskListEditor.CurrentTask;

                        if (currentTask == null)
                        {
                            currentTask = scenario.TaskListEditor.Tasks.FirstOrDefault();
                            if (currentTask != null)
                            {
                                currentTask.IsVisible = true;
                            }
                        }
                        else
                        {
                            scenario.SetCameraTo(currentTask.Satellite);
                        }
                    }
                    break;
                    default:
                        break;
                }
            });
        }

        [Reactive]
        public Workspace ActiveWorkspace { get; set; } = Workspace.Layout;
    }
}
