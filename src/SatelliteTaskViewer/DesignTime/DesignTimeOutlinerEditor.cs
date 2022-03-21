using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
