using FlashbackMaps.Application.Service;
using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;

namespace FlashbackMaps.Data.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imagesRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imagesRepository = imageRepository;
        }

        public void Add(Image image)
        {
            _imagesRepository.AddImage(image);
        }

        public void Delete(long id)
        {
            _imagesRepository.DeleteImage(id);
        }

        public IEnumerable<Image> GetImagesByLocationId(long locationId)
        {
            return _imagesRepository.GetByLocationId(locationId);
        }
    }
}
