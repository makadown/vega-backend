using Microsoft.AspNetCore.Mvc;
using vega_backend.Models;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        [HttpPost]
        public IActionResult CreateVehicle(Vehicle vehicle) {
            return Ok(vehicle);
        }
    }
}