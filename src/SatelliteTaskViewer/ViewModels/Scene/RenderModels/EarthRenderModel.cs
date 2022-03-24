using System.Collections.Generic;
using System.Collections.Immutable;
using SatelliteTaskViewer.ViewModels.Geometry;

namespace SatelliteTaskViewer.ViewModels.Scene
{
    public class EarthRenderModel : BaseRenderModel
    {
        public ImmutableArray<Mesh> Meshes { get; set; }

        // (string pos_x, string neg_z, string neg_x, string pos_z, string pos_y, string neg_y)
        public IEnumerable<string> DiffuseKeys { get; set; } = new List<string>();

        public IEnumerable<string> SpecularKeys { get; set; } = new List<string>();

        public IEnumerable<string> NormalKeys { get; set; } = new List<string>();

        public IEnumerable<string> NightKeys { get; set; } = new List<string>();
    }
}
