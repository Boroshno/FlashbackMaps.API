using FlashbackMaps.Domain;
using FlashbackMaps.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashbackMaps.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {

        private readonly ILogger<LocationsController> _logger;
        private readonly ILocationsService _locationsService;
        public LocationsController(ILogger<LocationsController> logger, ILocationsService locationsService)
        {
            _logger = logger;
            _locationsService = locationsService;
        }

        [HttpGet]
        public IEnumerable<Location> Get()
        {
            return _locationsService.GetLocations();
        }

        [HttpGet("{id}")]
        public Location Get(long id)
        {
            return _locationsService.GetLocations(id);
        }

        [HttpPost]
        public void Post([FromBody] Location location)
        {
            _locationsService.AddLocation(location);
        }
    }
}