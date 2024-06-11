

using FlashbackMaps.Domain;

namespace FlashbackMaps.Data.Repositories
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll();
        Location GetById(long id);
        Location Add(Location location);
        Location Delete(long id);
    }
}
