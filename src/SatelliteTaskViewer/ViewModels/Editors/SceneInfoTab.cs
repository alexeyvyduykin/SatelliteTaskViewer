using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SatelliteTaskViewer.ViewModels.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SatelliteTaskViewer.ViewModels.Editors
{
    public class SceneInfoTab : ViewModelBase
    {
        private readonly OutlinerEditorViewModel _outliner;
        private readonly Scenario _scenario;

        public SceneInfoTab(Scenario scenario)
        {
            _scenario = scenario;
            _outliner = scenario.OutlinerEditor ?? throw new Exception();

            var logical = _outliner.FrameRoot;
            var visual = _outliner.Entities;

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
                    EarthInfo = EarthInfo.BuildFrom(earth, _scenario);
                    break;
                case Satellite satellite:
                    SatelliteInfos.Add(SatelliteInfo.BuildFrom(satellite, _scenario));
                    break;
                case Retranslator retranslator:
                    RetranslatorInfos.Add(RetranslatorInfo.BuildFrom(retranslator));
                    break;
                case GroundStation groundStation:
                    GroundStationInfos.Add(GroundStationInfo.BuildFrom(groundStation));
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
        public static EarthInfo BuildFrom(Earth earth, Scenario scenario)
        {
            EarthInfo info = new EarthInfo();

            info.Name = earth.Name;

            info.IsVisible = earth.IsVisible;
                
            info.IsFrameVisible = earth.Frame.IsVisible;
            
            info.IsCameraTarget = (scenario.SceneState!.Target == earth);

            info.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
            {
                earth.IsVisible = visible;
            });

            info.WhenAnyValue(s => s.IsFrameVisible).Subscribe(frameVisible =>
            {
                earth.Frame.IsVisible = frameVisible;
            });

            info.WhenAnyValue(s => s.IsCameraTarget).Subscribe(cameraTarget =>
            {
                if (cameraTarget == true)
                {
                    scenario.SetCameraTo(earth);
                }
            });

            return info;
        }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }

        [Reactive]
        public bool IsCameraTarget { get; set; }
    }

    public class SatelliteInfo : ViewModelBase
    {
        public static SatelliteInfo BuildFrom(Satellite satellite, Scenario scenario)
        {
            SatelliteInfo info = new SatelliteInfo();

            var orbit = satellite.Children.Where(s => s is Orbit).Cast<Orbit>().FirstOrDefault();

            info.Name = satellite.Name;

            info.IsVisible = satellite.IsVisible;

            info.IsFrameVisible = satellite.Frame.IsVisible;

            info.IsCameraTarget = (scenario.SceneState!.Target == satellite);

            info.IsOrbitVisible = (orbit != null);

            info.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
            {
                satellite.IsVisible = visible;
            });

            info.WhenAnyValue(s => s.IsFrameVisible).Subscribe(frameVisible =>
            {
                satellite.Frame.IsVisible = frameVisible;
            });

            info.WhenAnyValue(s => s.IsCameraTarget).Subscribe(cameraTarget =>
            {
                if (cameraTarget == true)
                {
                    scenario.SetCameraTo(satellite);
                }
            });

            info.WhenAnyValue(s => s.IsOrbitVisible).Subscribe(orbitVisible =>
            {
                if (orbit != null)
                {
                    orbit.IsVisible = orbitVisible;
                }
            });

            return info;
        }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }

        [Reactive]
        public bool IsCameraTarget { get; set; }

        [Reactive]
        public bool IsOrbitVisible { get; set; }
    }

    public class RetranslatorInfo : ViewModelBase
    {
        public static RetranslatorInfo BuildFrom(Retranslator retranslator)
        {
            RetranslatorInfo info = new RetranslatorInfo();

            info.Name = retranslator.Name;

            info.IsVisible = retranslator.IsVisible;

            info.IsFrameVisible = retranslator.Frame.IsVisible;

            info.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
            {
                retranslator.IsVisible = visible;
            });

            info.WhenAnyValue(s => s.IsFrameVisible).Subscribe(frameVisible =>
            {
                retranslator.Frame.IsVisible = frameVisible;
            });

            return info;
        }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }

    public class GroundStationInfo : ViewModelBase
    {
        public static GroundStationInfo BuildFrom(GroundStation groundStation)
        {
            GroundStationInfo info = new GroundStationInfo();

            info.Name = groundStation.Name;

            info.IsVisible = groundStation.IsVisible;

            info.IsFrameVisible = groundStation.Frame.IsVisible;

            info.WhenAnyValue(s => s.IsVisible).Subscribe(visible =>
            {
                groundStation.IsVisible = visible;
            });

            info.WhenAnyValue(s => s.IsFrameVisible).Subscribe(frameVisible =>
            {
                groundStation.Frame.IsVisible = frameVisible;
            });

            return info;
        }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsFrameVisible { get; set; }
    }
}
