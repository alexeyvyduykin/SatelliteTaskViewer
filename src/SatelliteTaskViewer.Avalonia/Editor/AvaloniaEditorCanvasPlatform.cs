using System;
using SatelliteTaskViewer.Models.Editor;
using SatelliteTaskViewer.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.Avalonia.Editor
{
    public class AvaloniaEditorCanvasPlatform : ViewModelBase, IEditorCanvasPlatform
    {
        private readonly IServiceProvider _serviceProvider;

        public AvaloniaEditorCanvasPlatform(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [Reactive]
        public Action? InvalidateControl { get; set; }
    }
}
