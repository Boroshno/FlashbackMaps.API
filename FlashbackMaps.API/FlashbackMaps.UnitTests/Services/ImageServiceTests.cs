using Moq;
using FlashbackMaps.Data.Services;
using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;

public class ImageServiceTests
{
    private readonly Mock<IImageRepository> _mockImageRepository;
    private readonly ImageService _service;

    public ImageServiceTests()
    {
        _mockImageRepository = new Mock<IImageRepository>();
        _service = new ImageService(_mockImageRepository.Object);
    }

    [Fact]
    public void GetImagesByLocationId_ReturnsExpectedImages()
    {
        // Arrange
        var locationId = 1;
        var expectedImages = new List<Image> { new Image(), new Image() };
        _mockImageRepository.Setup(repo => repo.GetByLocationId(locationId)).Returns(expectedImages);

        // Act
        var result = _service.GetImagesByLocationId(locationId);

        // Assert
        Assert.Equal(expectedImages, result);
    }

    [Fact]
    public void AddImage_CallsAddOnImageRepository()
    {
        // Arrange
        var image = new Image();

        // Act
        _service.Add(image);

        // Assert
        _mockImageRepository.Verify(repo => repo.AddImage(image), Times.Once);
    }

    [Fact]
    public void DeleteImage_CallsDeleteOnImageRepository()
    {
        // Arrange
        var imageId = 1;

        // Act
        _service.Delete(imageId);

        // Assert
        _mockImageRepository.Verify(repo => repo.DeleteImage(imageId), Times.Once);
    }
}
