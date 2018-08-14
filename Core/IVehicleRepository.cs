using System.Threading.Tasks;
using vega_backend.Core.Models;

namespace vega_backend.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle (int id, bool includeRelated = true);
         void Add(Vehicle vehicle);
         void Remove(Vehicle vehicle);
    }
}