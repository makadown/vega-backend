using System.Threading.Tasks;

namespace vega_backend.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}