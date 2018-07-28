using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega_backend.Models;
using vega_backend.Persistence;

namespace vega_backend.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        public MakesController(VegaDbContext context)
        {
            this._context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<Make>> GetMakes() {
            // El include hace que se populen los modelos para cada Make
            return await this._context.Makes.Include( m => m.Models ).ToListAsync();
        }

    }
}