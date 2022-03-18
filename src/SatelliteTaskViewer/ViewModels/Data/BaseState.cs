using GlmSharp;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class BaseState : ViewModelBase
    {       
        [Reactive]
        public dmat4 ModelMatrix { get; protected set; }
       
        [Reactive]
        public BaseState? Parent { get; set; }

        public dmat4 AbsoluteModelMatrix => GetAbsoluteModelMatrix();

        protected dmat4 GetAbsoluteModelMatrix()
        {
            var state = Parent;
            var modelMatrix = ModelMatrix;

            while (state is not null)
            {
                modelMatrix = state.ModelMatrix * modelMatrix;

                state = state.Parent;
            }

            return modelMatrix;
        }
    }
}
