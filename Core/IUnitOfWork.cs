using System.Threading.Tasks;

namespace vega_backend.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}