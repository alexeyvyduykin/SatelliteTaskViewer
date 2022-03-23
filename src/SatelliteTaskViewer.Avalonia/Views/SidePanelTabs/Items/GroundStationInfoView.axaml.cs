using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SatelliteTaskViewer.Avalonia.Views.SidePanelTabs.Items
{
    public partial class GroundStationInfoView : UserControl
    {
        public GroundStationInfoView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
