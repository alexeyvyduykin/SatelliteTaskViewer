using GlmSharp;
using System.Collections.Generic;

namespace SatelliteTaskViewer.ViewModels.Geometry
{
    public record Mesh
    {
        public IList<vec3> Vertices { get; init; } = new List<vec3>();

        public IList<vec3> Normals { get; init; } = new List<vec3>();

        public IList<vec2> TexCoords { get; init; } = new List<vec2>();

        public IList<vec3> Tangents { get; init; } = new List<vec3>();

        public IList<ushort> Indices { get; init; } = new List<ushort>();

        // public Material Material { get; init; }

        public int MaterialIndex { get; init; }
    }
}
