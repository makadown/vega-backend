using Microsoft.EntityFrameworkCore;
using vega_backend.Models;

namespace vega_backend.Persistence
{
    public class VegaDbContext : DbContext
    {
        
/*El meter al menos un DbSet aquí hace que se agregue / actualice la migracion */
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features {get; set;}
        public VegaDbContext( DbContextOptions<VegaDbContext> options) : base( options )
        {
            // System.Configuration.ConfigurationManager
        }

/* Como en el modelo no hay anotaciones para llaves compuestas, se tienen que crear en el evento 
OnModelCreating */
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<VehicleFeature>().HasKey( vf => new {vf.VehicleId, vf.FeatureId } );
        }

    }
}