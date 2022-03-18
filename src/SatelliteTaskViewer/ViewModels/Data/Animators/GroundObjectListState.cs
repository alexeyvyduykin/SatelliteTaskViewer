using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class GroundObjectListState : BaseState
    {
        public GroundObjectListState(IDictionary<string, GroundObjectState> states)
        {
            States = states;
        }

        [Reactive]
        public IDictionary<string, GroundObjectState> States { get; protected set; }
    }
}
