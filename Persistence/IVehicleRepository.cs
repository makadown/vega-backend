using System.Threading.Tasks;
using vega_backend.Models;

namespace vega_backend.Persistence
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle (int id);
    }
}