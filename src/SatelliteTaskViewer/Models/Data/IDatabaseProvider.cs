using System.Threading.Tasks;

namespace SatelliteTaskViewer.Models.Data
{
    public interface IDatabaseProvider : IDataProvider
    {
        Task Save();
    }
}
