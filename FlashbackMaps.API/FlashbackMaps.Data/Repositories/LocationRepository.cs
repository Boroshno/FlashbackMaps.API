using FlashbackMaps.Domain;

namespace FlashbackMaps.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Set<Location>().ToList();
        }

        public Location GetById(long id)
        {
            return _context.Set<Location>().Find(id);
        }

        public Location Add(Location location)
        {
            _context.Set<Location>().Add(location);
            _context.SaveChanges();
            return location;
        }

        public Location Delete(long id)
        {
            var location = _context.Set<Location>().Find(id);
            if (location == null)
            {
                return null;
            }

            _context.Set<Location>().Remove(location);
            _context.SaveChanges();
            return location;
        }
    }

}
