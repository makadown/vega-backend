using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_backend.Core;
using vega_backend.Core.Models;

namespace vega_backend.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private VegaDbContext context ;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle (int id, bool includeRelated = true) {

            if (!includeRelated) {
                return await context.Vehicles.FindAsync(id);
            }

            return await context.Vehicles
                                .Include(v => v.Features)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                    .ThenInclude( m => m.Make )
                                .SingleOrDefaultAsync( v => v.Id == id);
        }

        public void Add(Vehicle vehicle) {
            context.Vehicles.Add(vehicle);
        }
         public void Remove(Vehicle vehicle) {
            context.Vehicles.Remove(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
        return await context.Vehicles
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .ToListAsync();
        }
        
    }
}