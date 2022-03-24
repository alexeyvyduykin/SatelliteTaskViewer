using SatelliteTaskViewer.ViewModels.Editors;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeOutlinerEditor : OutlinerEditorViewModel
    {
        public DesignTimeOutlinerEditor() : base(new DesignTimeData().GetScenario())
        {

        }
    }
}
