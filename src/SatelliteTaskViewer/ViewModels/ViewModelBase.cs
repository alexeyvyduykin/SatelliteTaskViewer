using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        [Reactive]
        public virtual ViewModelBase? Owner { get; set; }

        [Reactive]
        public virtual string Name { get; set; } = string.Empty;
    }
}
