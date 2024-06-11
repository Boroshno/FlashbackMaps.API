using FlashbackMaps.Domain;

namespace FlashbackMaps.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetByLocationId(long locationId)
        {
            return _context.Set<Image>().Where(i => i.LocationId == locationId).ToList();
        }

        public void AddImage(Image image)
        {
            _context.Set<Image>().Add(image);
            _context.SaveChanges();
        }

        public void DeleteImage(long id)
        {
            var image = _context.Set<Image>().Find(id);
            if (image == null)
            {
                return;
            }

            _context.Set<Image>().Remove(image);
            _context.SaveChanges();
        }
    }
}
