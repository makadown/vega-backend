using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vega_backend.Controllers.Resources;
using vega_backend.Core;
using vega_backend.Core.Models;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] {".jpg", ".jpeg", ".png"};
        private readonly IMapper mapper;
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        public readonly IUnitOfWork unitOfWork;

        public PhotosController(IMapper mapper, 
                                IHostingEnvironment host, IVehicleRepository repository, 
                                IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file) 
        {
            var vehicle = await repository.GetVehicle(vehicleId, includeRelated:false);
            if (vehicle== null)
                return NotFound();

            if ( file == null ) return BadRequest("Null file");
            if (file.Length==0) return BadRequest("Empty File");            
            if (file.Length > MAX_BYTES) return BadRequest("Maximum file size exceeded");            
            if ( !ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName ) )) 
                  return BadRequest("Invalid File Type" );

             var uploadsFolderPath = Path.Combine(host.WebRootPath,"uploads");
             if (!Directory.Exists(uploadsFolderPath))
             {
                 Directory.CreateDirectory(uploadsFolderPath);
             }
             // Para generar el nombre del nuevo archivo que se subira en el servidor
             var fileName = Guid.NewGuid().ToString() + 
                        Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create) )
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}