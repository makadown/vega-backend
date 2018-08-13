using System.Threading.Tasks;
using vega_backend.Models;

namespace vega_backend.Persistence
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle (int id, bool includeRelated = true);
         void Add(Vehicle vehicle);
         void Remove(Vehicle vehicle);
    }
}