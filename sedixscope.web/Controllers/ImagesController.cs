using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository _imagesRepository;
        public ImagesController(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imagesRepository.UploadAsync(file);

            if (imageUrl != null)
            {
                return new JsonResult(new {link = imageUrl});
            }

            return Problem("Something went wrong on our side. Please contact the developer.", null, (int)HttpStatusCode.InternalServerError);
        }
    }
}