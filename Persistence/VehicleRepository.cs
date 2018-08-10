using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_backend.Models;

namespace vega_backend.Persistence
{
    public class VehicleRepository
    {
        private VegaDbContext context ;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle (int id) {
            return await context.Vehicles
                                .Include(v => v.Features)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                    .ThenInclude( m => m.Make )
                                .SingleOrDefaultAsync( v => v.Id == id);
        }
    }
}