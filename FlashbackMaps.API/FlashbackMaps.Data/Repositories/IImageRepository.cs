using FlashbackMaps.Domain;
namespace FlashbackMaps.Data.Repositories
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetByLocationId(long locationId);
        void AddImage(Image image);
        void DeleteImage(long id);
    }
}
