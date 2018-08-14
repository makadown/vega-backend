using System.Collections.Generic;
using System.Collections.ObjectModel;
using vega_backend.Core.Models;

namespace vega_backend.Controllers.Resources
{
    /**
    * Esta clase se parece a la del modelo, pero no tiene las anotaciones.
    * Se utiliza junto con todo en Resources para ser utilizado
    * al consumir APIs
    */
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}