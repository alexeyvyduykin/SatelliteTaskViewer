using SatelliteTaskViewer.ViewModels;

namespace SatelliteTaskViewer.Models.Renderer
{
    public interface IContainerPresenter
    {
        void Render(object dc, IRenderContext renderer, Scenario scenario);
    }
}
