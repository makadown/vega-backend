using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using vega_backend.Controllers.Resources;
using vega_backend.Core;
using vega_backend.Core.Models;

namespace vega_backend.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {        
        private readonly PhotoSettings photoSettings;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IPhotoRepository photoRepository;
        public readonly IUnitOfWork unitOfWork;

        public PhotosController(IMapper mapper, 
                                IHostingEnvironment host, IVehicleRepository repository, 
                                IPhotoRepository photoRepository,
                                IUnitOfWork unitOfWork, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.photoRepository = photoRepository;
            this.repository = repository;
        }

        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId) {
            var photos = await photoRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file) 
        {
            var vehicle = await repository.GetVehicle(vehicleId, includeRelated:false);
            if (vehicle== null)
                return NotFound();

            if ( file == null ) return BadRequest("Null file");
            if (file.Length==0) return BadRequest("Empty File");            
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Maximum file size exceeded");            
            
            if ( !photoSettings.IsSupported(file.FileName) ) return BadRequest("Invalid File Type" );

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