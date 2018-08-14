using System.Threading.Tasks;
using vega_backend.Core;

namespace vega_backend.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }

       public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}