using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Containers
{
    public abstract class BaseContainerViewModel : ViewModelBase
    {
        protected BaseContainerViewModel() { }

        [Reactive]
        public bool IsVisible { get; set; }

        [Reactive]
        public bool IsExpanded { get; set; }
    }
}
