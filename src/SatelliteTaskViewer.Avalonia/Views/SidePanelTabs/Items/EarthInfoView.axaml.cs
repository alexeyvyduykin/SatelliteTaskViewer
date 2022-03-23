using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SatelliteTaskViewer.Avalonia.Views.SidePanelTabs.Items
{
    public partial class EarthInfoView : UserControl
    {
        public EarthInfoView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
