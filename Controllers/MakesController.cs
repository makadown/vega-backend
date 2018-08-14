using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega_backend.Core.Models;
using vega_backend.Persistence;
using AutoMapper;
using vega_backend.Controllers.Resources;

namespace vega_backend.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this._context = context;

        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            
            /* El operador await suspende el metodo GetMakes() hasta que se complete
               el metodo invocado por _context.Makes.Include( m => m.Models ).ToListAsync() */
            var makes  = await this._context.Makes.Include(m => m.Models).ToListAsync();
            /* El include hace que se populen los modelos para cada Make */
            
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
             /*                  Origen ,   Destino          (origen)
                Esto retornara List<MakeResource> */


            /* NOTA: Tambien podria funcionar solamente con 
                     await this._context.Makes.Include(m => m.Models).ToListAsync();
            usando solo los modelos sin usar los resources / automapper
            y que el metodo retorne un 
                      Task<IEnumerable<Make>>
             */
        }

    }
}