using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using vega_backend.Core;
using vega_backend.Core.Models;
using vega_backend.Controllers.Resources;
using System.Collections.Generic;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    { 
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController( IMapper mapper, IVehicleRepository repository, 
                                   IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }       

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource){

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

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

            await this.unitOfWork.CompleteAsync();

            // Vuelvo a obtener pa amarrar bien y evitar un bug
            vehicle = await repository.GetVehicle(vehicle.Id);
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
            await this.unitOfWork.CompleteAsync();

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

        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource); 
            var queryResultObject = await repository.GetVehicles(filter);
            return mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResultObject);
        }
    }
}