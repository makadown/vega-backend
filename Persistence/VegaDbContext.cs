using Microsoft.EntityFrameworkCore;
using vega_backend.Models;

namespace vega_backend.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext( DbContextOptions<VegaDbContext> options) : base( options )
        {
            // System.Configuration.ConfigurationManager
        }

/*El meter al menos un DbSet aquí hace que se agregue / actualice la migracion */
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features {get; set;}
    }
}