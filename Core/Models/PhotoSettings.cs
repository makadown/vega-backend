using System.Linq;
using System.IO;

namespace vega_backend.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string filename) {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(filename).ToLower() );
        }
    }
}