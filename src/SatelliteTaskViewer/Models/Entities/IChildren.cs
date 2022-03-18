using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteTaskViewer.ViewModels.Entities;

namespace SatelliteTaskViewer.Models.Entities
{
    public interface IChildren
    {
        ImmutableArray<BaseEntity> Children { get; set; }
    }
}
