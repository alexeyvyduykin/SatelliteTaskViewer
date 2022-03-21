using SatelliteTaskViewer.Timer;
using SatelliteTaskViewer.ViewModels.Editors;
using System;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeSceneTimerEditor : SceneTimerEditorViewModel
    {
        public DesignTimeSceneTimerEditor() : base(new AcceleratedTimer(), DateTime.Now, TimeSpan.FromDays(1))
        {

        }
    }
}
