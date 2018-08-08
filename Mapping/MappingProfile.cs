using AutoMapper;
using System.Linq;
using vega_backend.Controllers.Resources;
using vega_backend.Models;
using System.Collections;
using System.Collections.Generic;

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
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, 
                           opt => opt.MapFrom( v => new ContactResource 
                                      {Name= v.ContactName, Email=v.ContactEmail, Phone=v.ContactPhone}
                                           ) 
                        )
                .ForMember( vr => vr.Features,
                            opt => opt.MapFrom( v => v.Features.Select( vf => vf.FeatureId ) ) );

            // From API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore() )      /* Para evitar error al actualizar */
                .ForMember(v  => v.ContactName, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Name ) )
                .ForMember(v  => v.ContactEmail, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Email ) )
                .ForMember(v  => v.ContactPhone, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Phone ) )
                .ForMember(v  => v.Features, opt=> opt.Ignore() )
                .AfterMap( (vr, v) => {
                    // Remove selected features
                    var removedFeatures = new List<VehicleFeature>();
                    foreach (var f in v.Features) {
                        if ( !vr.Features.Contains(f.FeatureId)) {
                            removedFeatures.Add(f);
                        }
                    }
                    foreach (var f in removedFeatures ) {
                        v.Features.Remove(f);
                    }

                    // Add new features
                     foreach (var id in vr.Features) {
                        if ( !v.Features.Any( f=> f.FeatureId == id ) ) {
                            v.Features.Add( new VehicleFeature { FeatureId = id } );
                        }
                    }
                });

        }
    }
}