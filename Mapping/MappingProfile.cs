using AutoMapper;
using System.Linq;
using vega_backend.Controllers.Resources;
using vega_backend.Models;

namespace vega_backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            // From API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v  => v.ContactName, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Name ) )
                .ForMember(v  => v.ContactEmail, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Email ) )
                .ForMember(v  => v.ContactPhone, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Phone ) )
                .ForMember(v  => v.Features, 
                           opt=> opt.MapFrom( vr=> vr.Features.Select(id => new VehicleFeature { FeatureId = id } ) ) );

        }
    }
}