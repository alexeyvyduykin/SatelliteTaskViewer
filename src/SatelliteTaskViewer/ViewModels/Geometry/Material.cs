using GlmSharp;

namespace SatelliteTaskViewer.ViewModels.Geometry
{
    public record Material
    {
        public vec4 Ambient { get; init; }

        public vec4 Diffuse { get; init; }

        public vec4 Specular { get; init; }

        public vec4 Emission { get; init; }

        public float Shininess { get; init; }

        public bool HasTextureDiffuse { get; init; }

        public string TextureDiffusePath { get; init; } = string.Empty;

        public string TextureDiffuseKey { get; init; } = string.Empty;

        //   public ITexture MapDiffuse { get; init; }
    }
}
