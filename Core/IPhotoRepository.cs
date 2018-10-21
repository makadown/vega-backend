using System.Collections.Generic;
using System.Threading.Tasks;
using vega_backend.Core.Models;
using System.Linq;

namespace vega_backend.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}