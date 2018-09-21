using AutoMapper;
using System.Linq;
using vega_backend.Controllers.Resources;
using vega_backend.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace vega_backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Photo, PhotoResource>();

            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, 
                           opt => opt.MapFrom( v => new ContactResource 
                                      {Name= v.ContactName, Email=v.ContactEmail, Phone=v.ContactPhone}
                                           ) 
                        )
                .ForMember( vr => vr.Features,
                            opt => opt.MapFrom( v => v.Features.Select( vf => vf.FeatureId ) ) );
                            
            CreateMap<Vehicle, VehicleResource>()
                 .ForMember( vr => vr.Make, opt => opt.MapFrom( v => v.Model.Make) )
                 .ForMember(vr => vr.Contact, 
                           opt => opt.MapFrom( v => new ContactResource 
                                      {Name= v.ContactName, Email=v.ContactEmail, Phone=v.ContactPhone}
                                           ) 
                        )
                 .ForMember( vr => vr.Features,
                            opt => opt.MapFrom( v => v.Features.Select( vf => new KeyValuePairResource 
                                             { Id =vf.Feature.Id, Name = vf.Feature.Name } ) ) );

            // From API Resource to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();

            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore() )      /* Para evitar error al actualizar */
                .ForMember(v  => v.ContactName, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Name ) )
                .ForMember(v  => v.ContactEmail, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Email ) )
                .ForMember(v  => v.ContactPhone, 
                           opt=> opt.MapFrom( vr=> vr.Contact.Phone ) )
                .ForMember(v  => v.Features, opt=> opt.Ignore() )
                .AfterMap( (vr, v) => {
                    // Remove selected features.
                    /* Recorro features que estan en el registro a crear/actualizar del vehiculo (v) 
                       que no estan en el resource (vr) para borrarlos.
                    */
                    var removedFeatures = v.Features.Where(f=>!vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures ) {
                        v.Features.Remove(f);
                    }

                    // Add new features
                   /* Recorro features que estan en el resource (vr) que no estan en el 
                     registro a crear/actualizar del vehiculo (v) para agregarlos.
                    */
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any( f=> f.FeatureId == id ) )
                                 .Select(id => new VehicleFeature { FeatureId = id }).ToList();

                    foreach(var f in addedFeatures) {
                        v.Features.Add( f );
                    }
                });

        }
    }
}