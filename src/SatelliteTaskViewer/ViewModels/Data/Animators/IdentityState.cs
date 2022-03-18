using GlmSharp;
using SatelliteTaskViewer.Models;

namespace SatelliteTaskViewer.ViewModels.Data
{
    public class IdentityState : BaseState, IFrameable
    {
        public IdentityState()
        {
            ModelMatrix = dmat4.Identity;
        }
    }
}
