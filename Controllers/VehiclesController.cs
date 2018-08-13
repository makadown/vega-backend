using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vega_backend.Controllers.Resources;
using vega_backend.Models;
using AutoMapper;
using vega_backend.Persistence;
using System;
using Microsoft.EntityFrameworkCore;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;

        public VehiclesController(VegaDbContext context, 
                IMapper mapper, IVehicleRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.context = context;

        }       

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource savevehicleResource){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Esta validacion es a modo de ejercicio. PEro normalmente, como todo se invocaria desde
               el ambiente controlado de un frontend no se necesita. */
            var model = await context.Models.FindAsync(savevehicleResource.ModelId);
            if (model == null) {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }
            if ( savevehicleResource.Features != null )
            {
                foreach (var featureId in savevehicleResource.Features)
                {
                      var feat  = await context.Features.FindAsync(featureId);
                      if (feat == null) {
                            ModelState.AddModelError("FeaturelId", errorMessage: "Invalid FeatureId");
                            return BadRequest(ModelState);
                        }
                }
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(savevehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await context.SaveChangesAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);
            /* Para devolver el vehicleResource en lugar del vehicle (o del SaveVehicleResource),  
               Además, si intento regresar el puro vehicle, obtendré error de
               cochinero circular.
            */
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")] //   /api/vehicles/{id}
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource savevehicleResource){

            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            // busco vehiculo en la BD
            var vehicle = await repository.GetVehicle(id);

             if (vehicle == null) 
            {
                return NotFound();
            }

            // mapeo el vehiculo con el vehicleReource. Se respeta el id de vehicle
            mapper.Map<SaveVehicleResource, Vehicle>(savevehicleResource, vehicle);

            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();
            // mapeo ahora con VehicleResource (no con SaveVehicleResource) para devolver detalle
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id) {
            
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null) 
            {
                return NotFound();
            }

            repository.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id) {
            
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null) 
            {
                return NotFound();
            }

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}