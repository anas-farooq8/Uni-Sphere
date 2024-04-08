using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _ImageRepository;

        public ImagesController(IImageRepository ImageRepository)
        {
            _ImageRepository = ImageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _ImageRepository.UploadAsync(file);
            if(imageUrl == null )
            {
                return Problem("Something Went Wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl });
        }

    }
}
