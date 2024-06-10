
using FlashbackMaps.Domain;

namespace FlashbackMaps.Application.Service
{
    public interface ILocationsService
    {
        IEnumerable<Location> GetLocations();
        Location GetLocations(long id);
        void AddLocation(Location location);
    }
}
