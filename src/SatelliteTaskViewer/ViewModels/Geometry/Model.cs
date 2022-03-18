using System.Collections.Generic;

namespace SatelliteTaskViewer.ViewModels.Geometry
{
    public record Model
    {
        public IList<Mesh> Meshes { get; init; }

        public IList<Material> Materials { get; init; }
    }
}
