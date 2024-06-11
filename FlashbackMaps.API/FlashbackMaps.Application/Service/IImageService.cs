using FlashbackMaps.Domain;

namespace FlashbackMaps.Application.Service
{
    public interface IImageService
    {
        void Add(Image image);
        void Delete(long id);
        IEnumerable<Image> GetImagesByLocationId(long locationId);
    }
}
