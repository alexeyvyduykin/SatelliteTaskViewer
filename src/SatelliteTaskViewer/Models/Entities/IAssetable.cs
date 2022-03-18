using System;
using System.Collections.Immutable;
using System.Text;
using SatelliteTaskViewer.ViewModels.Entities;

namespace SatelliteTaskViewer.Models.Entities
{
    public interface IAssetable<T> 
    {
        ImmutableArray<T> Assets { get; set; }
    }
}
