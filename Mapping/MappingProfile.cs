using AutoMapper;
using vega_backend.Controllers.Resources;
using vega_backend.Models;

namespace vega_backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}