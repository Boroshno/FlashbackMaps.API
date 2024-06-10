using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;
namespace FlashbackMaps.Application.Service
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void AddLocation(Location location)
        {
            _locationRepository.Add(location);
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locationRepository.GetAll();
        }

        public Location GetLocations(long id)
        {
            return _locationRepository.GetById(id);
        }
    }
}
