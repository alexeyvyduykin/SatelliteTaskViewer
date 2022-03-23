using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public class SceneInfoTab : ViewModelBase
    {
        private readonly OutlinerEditorViewModel _outliner;

        public SceneInfoTab(OutlinerEditorViewModel outliner)
        {
            _outliner = outliner;

            var logical = outliner.FrameRoot;
            var visual = outliner.Entities;

            if (visual != null)
            {
                foreach (var item in visual)
                {
                    if (item is EntityList list)
                    {
                        foreach (var itemList in list.Values)
                        {
                            Build(itemList);
                        }
                    }
                    else
                    {
                        Build(item);
                    }
                }
            }
        }

        private void Build(BaseEntity entity)
        {
            switch (entity)
            {
                case Earth earth:
                {
                    EarthInfo = new EarthInfo()
                    {
                        Name = earth.Name,
                        IsVisible = earth.IsVisible,
                    };

                    EarthInfo.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
                    {
                        earth.IsVisible = visible;
                    });
                }                    
                break;
                case Satellite satellite:
                {
                    var satelliteInfo = new SatelliteInfo()
                    {
                        Name = satellite.Name,
                        IsVisible = satellite.IsVisible,
                    };

                    satelliteInfo.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
                    {
                        satellite.IsVisible = visible;
                    });

                    SatelliteInfos.Add(satelliteInfo);
                }                    
                break;
                case Retranslator retranslator:
                {
                    var retranslatorInfo = new RetranslatorInfo()
                    {
                        Name = retranslator.Name,
                        IsVisible = retranslator.IsVisible,
                    };

                    retranslatorInfo.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
                    {
                        retranslator.IsVisible = visible;
                    });

                    RetranslatorInfos.Add(retranslatorInfo);
                }                    
                break;
                case GroundStation groundStation:
                {
                    var groundStationInfo = new GroundStationInfo()
                    {
                        Name = groundStation.Name,
                        IsVisible = groundStation.IsVisible,
                    };

                    groundStationInfo.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
                    {
                        groundStation.IsVisible = visible;
                    });

                    GroundStationInfos.Add(groundStationInfo);
                }                   
                break;
            }
        }

        [Reactive]
        public EarthInfo? EarthInfo { get; protected set; }

        [Reactive]
        public ObservableCollection<SatelliteInfo> SatelliteInfos { get; protected set; } = new ObservableCollection<SatelliteInfo>();

        [Reactive]
        public ObservableCollection<RetranslatorInfo> RetranslatorInfos { get; protected set; } = new ObservableCollection<RetranslatorInfo>();

        [Reactive]
        public ObservableCollection<GroundStationInfo> GroundStationInfos { get; protected set; } = new ObservableCollection<GroundStationInfo>();
    }


    public class EarthInfo : ViewModelBase
    {
        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }

    public class SatelliteInfo : ViewModelBase
    {
        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }

    public class RetranslatorInfo : ViewModelBase
    {
        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }

    public class GroundStationInfo : ViewModelBase
    {
        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }
}
