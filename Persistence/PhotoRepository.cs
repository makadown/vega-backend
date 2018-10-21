using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_backend.Core;
using vega_backend.Core.Models;
using vega_backend.Extensions;

namespace vega_backend.Persistence
{
    public class PhotoRepository: IPhotoRepository
    {
        private VegaDbContext context ;
        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId) {
            return await context.Photos
                    .Where(p => p.VehicleId == vehicleId)
                    .ToListAsync();
        }


    }
}