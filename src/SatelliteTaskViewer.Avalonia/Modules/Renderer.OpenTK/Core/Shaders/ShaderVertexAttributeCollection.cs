using System.Collections.ObjectModel;

namespace SatelliteTaskViewer.Avalonia.Renderer.OpenTK.Core
{
    internal class ShaderVertexAttributeCollection : KeyedCollection<string, ShaderVertexAttribute>
    {
        protected override string GetKeyForItem(ShaderVertexAttribute item)
        {
            return item.Name;
        }
    }
}
