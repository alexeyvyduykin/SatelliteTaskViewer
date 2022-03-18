using System.Collections.Immutable;
using SatelliteTaskViewer.ViewModels.Containers;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Entities
{
    public abstract class BaseEntity : BaseContainerViewModel
    {
        [Reactive]
        public ImmutableArray<BaseEntity> Children { get; set; }
    }
}
