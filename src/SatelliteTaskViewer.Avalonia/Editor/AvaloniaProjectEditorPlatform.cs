using SatelliteTaskViewer.Avalonia.Views;
using SatelliteTaskViewer.Models.Editor;
using System;

namespace SatelliteTaskViewer.Avalonia.Editor
{
    public class AvaloniaProjectEditorPlatform : IProjectEditorPlatform
    {
        private readonly IServiceProvider _serviceProvider;

        public AvaloniaProjectEditorPlatform(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
