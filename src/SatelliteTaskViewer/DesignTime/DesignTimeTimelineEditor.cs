using SatelliteTaskViewer.Timer;
using SatelliteTaskViewer.ViewModels.Editors;
using System;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeTimelineEditor : TimelineEditorViewModel
    {
        public DesignTimeTimelineEditor() : base()
        {
            SceneTimerEditor = new SceneTimerEditorViewModel(new AcceleratedTimer(), DateTime.Now, TimeSpan.FromDays(1));

            TaskListEditor = new TaskListEditorViewModel(DesignTimeTaskListEditor.CreateSatelliteTasks());
        }

        public SceneTimerEditorViewModel SceneTimerEditor { get; }

        public TaskListEditorViewModel TaskListEditor { get; }
    }
}
