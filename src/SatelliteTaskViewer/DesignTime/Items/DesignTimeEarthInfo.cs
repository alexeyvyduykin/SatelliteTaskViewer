using SatelliteTaskViewer.ViewModels.Editors;

namespace SatelliteTaskViewer.DesignTime
{
    public class DesignTimeEarthInfo : EarthInfo
    {
        public DesignTimeEarthInfo() : base()
        {
            Name = "Earth";

            IsVisible = true;
        }
    }
}
