using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Models.Editor
{
    public interface IEditorCanvasPlatform
    {
        Action? InvalidateControl { get; set; }

    }
}
