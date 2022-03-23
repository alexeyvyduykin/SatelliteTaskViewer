using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SatelliteTaskViewer.Avalonia.Views.SidePanelTabs
{
    public partial class SceneInfoTabView : UserControl
    {
        public SceneInfoTabView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
