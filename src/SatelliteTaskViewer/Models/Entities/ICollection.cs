using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Entities
{
    public interface ICollection<T> where T : class
    {
        ImmutableArray<T> Values { get; set; }
    }
}
