using Microsoft.EntityFrameworkCore;

namespace vega_backend.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext( DbContextOptions<VegaDbContext> options) : base( options )
        {
            // System.Configuration.ConfigurationManager
        }
    }
}