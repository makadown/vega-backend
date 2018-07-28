namespace vega_backend.Controllers.Resources
{
    /**
    * Esta clase tiene los campos como el modelo, pero sin
    * las anotaciones y sin la relación inversa.
    * Se utiliza junto con todo en Resources para ser utilizado
    * al consumir APIs
    */
    public class ModelResource
    {
        public int Id {get; set;}
        public string Name {get; set;}
    }
}