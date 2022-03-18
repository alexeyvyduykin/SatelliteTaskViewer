using System;
using System.Collections.Generic;
using System.Text;
//using SatelliteTaskViewer.Renderer;
using SatelliteTaskViewer.ViewModels.Containers;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IContainerPresenter
    {
        void Render(object dc, IRenderContext renderer, ScenarioContainerViewModel container);
    }
}
