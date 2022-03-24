using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Editors;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeSceneInfoTab : SceneInfoTab
    {
        public DesignTimeSceneInfoTab() : base(new ViewModels.Scenario() { OutlinerEditor = new DesignTimeOutlinerEditor() } /*new DesignTimeOutlinerEditor()*/)
        {
            EarthInfo = new EarthInfo() { Name = "Earth", IsVisible = true };

            SatelliteInfos = new ObservableCollection<SatelliteInfo>() 
            {
            new SatelliteInfo(){ Name = "Satellite1", IsVisible = false },
            new SatelliteInfo(){ Name = "Satellite2",IsVisible = true },
            new SatelliteInfo(){ Name = "Satellite3",IsVisible = false },
            new SatelliteInfo(){ Name = "Satellite4",IsVisible = true},
            new SatelliteInfo(){ Name = "Satellite5",IsVisible = false  },
            };

            RetranslatorInfos = new ObservableCollection<RetranslatorInfo>()
            {           
                new RetranslatorInfo(){ Name = "Retranslator1", IsVisible = false },            
                new RetranslatorInfo(){ Name = "Retranslator2", IsVisible = false },            
                new RetranslatorInfo(){ Name = "Retranslator3", IsVisible = true },
            };

            GroundStationInfos = new ObservableCollection<GroundStationInfo>() 
            {
            new GroundStationInfo(){ Name = "GroundStation1", IsVisible = true},
            new GroundStationInfo(){ Name = "GroundStation2", IsVisible = true},
            new GroundStationInfo(){ Name = "GroundStation3", IsVisible = false },
            new GroundStationInfo(){ Name = "GroundStation4", IsVisible = false },
            new GroundStationInfo(){ Name = "GroundStation5", IsVisible = true},
            new GroundStationInfo(){ Name = "GroundStation6", IsVisible = true},
            new GroundStationInfo(){ Name = "GroundStation7", IsVisible = false },
            new GroundStationInfo(){ Name = "GroundStation8", IsVisible = true},
            };
        }
    }
}
