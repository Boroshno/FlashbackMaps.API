using FlashbackMaps.Application.Service;
using FlashbackMaps.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FlashbackMaps.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IImageService _imagesService;

        public ImagesController(ILogger<ImagesController> logger, IImageService imagesService)
        {
            _logger = logger;
            _imagesService = imagesService;
        }

        [HttpGet("{locationId}")]
        public IEnumerable<Image> GetImageByLocationId(int locationId)
        {
            return _imagesService.GetImagesByLocationId(locationId);
        }

        [HttpPost]
        public void PostImage([FromBody] Image imageModel)
        {
            _imagesService.Add(imageModel);
        }

        [HttpDelete("{imageId}")]
        public void DeleteImage(int imageId)
        {
            _imagesService.Delete(imageId);
        }
    }
}
