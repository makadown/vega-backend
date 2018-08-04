using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vega_backend.Controllers.Resources;
using vega_backend.Models;
using AutoMapper;
using vega_backend.Persistence;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        
        private readonly VegaDbContext _context;
        private readonly IMapper mapper;
        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this._context = context;

        }       

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleResource vehicleResource){
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            return Ok(vehicle);
        }
    }
}