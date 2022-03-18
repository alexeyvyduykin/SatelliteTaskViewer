using System;
using System.Collections.Generic;
using System.Text;
using SatelliteTaskViewer.Models.Renderer;
using SatelliteTaskViewer.ViewModels.Containers;

namespace SatelliteTaskViewer.ViewModels.Renderer.Presenters
{
    public class EditorPresenter : IContainerPresenter
    {
        public void Render(object dc, IRenderContext renderer, ScenarioContainerViewModel container)
        {
            renderer.DrawScenario(dc, container);

            //if (container.WorkingLayer != null)
            //{
            //    renderer.DrawLayer(dc, container.WorkingLayer);
            //}

            //if (container.HelperLayer != null)
            //{
            //    renderer.DrawLayer(dc, container.HelperLayer);
            //}
        }
    }
}
