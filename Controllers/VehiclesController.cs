using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vega_backend.Controllers.Resources;
using vega_backend.Models;
using AutoMapper;
using vega_backend.Persistence;
using System;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }       

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Esta validacion es a modo de ejercicio. PEro normalmente, como todo se invocaria desde
               el ambiente controlado de un frontend no se necesita. */
            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if (model == null) {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }
            if ( vehicleResource.Features != null )
            {
                foreach (var featureId in vehicleResource.Features)
                {
                      var feat  = await context.Features.FindAsync(featureId);
                      if (feat == null) {
                            ModelState.AddModelError("FeaturelId", errorMessage: "Invalid FeatureId");
                            return BadRequest(ModelState);
                        }
                }
            }

            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            /* Para devolver el vehicleResource en lugar del vehicle,  
               Además, si intento regresar el puro vehicle, obtendré error de
               cochinero circular.
            */
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
    }
}