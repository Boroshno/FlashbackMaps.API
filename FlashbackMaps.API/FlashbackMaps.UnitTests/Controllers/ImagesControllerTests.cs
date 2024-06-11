using Moq;
using FlashbackMaps.API.Controllers;
using Microsoft.Extensions.Logging;
using FlashbackMaps.Domain;
using FlashbackMaps.Application.Service;

public class ImagesControllerTests
{
    private readonly Mock<ILogger<ImagesController>> _mockLogger;
    private readonly Mock<IImageService> _mockImageService;
    private readonly ImagesController _controller;

    public ImagesControllerTests()
    {
        _mockLogger = new Mock<ILogger<ImagesController>>();
        _mockImageService = new Mock<IImageService>();
        _controller = new ImagesController(_mockLogger.Object, _mockImageService.Object);
    }

    [Fact]
    public void GetImageByLocationId_ReturnsExpectedImages()
    {
        // Arrange
        var locationId = 1;
        var expectedImages = new List<Image> { new Image(), new Image() };
        _mockImageService.Setup(service => service.GetImagesByLocationId(locationId)).Returns(expectedImages);

        // Act
        var result = _controller.GetImageByLocationId(locationId);

        // Assert
        Assert.Equal(expectedImages, result);
    }

    [Fact]
    public void PostImage_CallsAddOnImageService()
    {
        // Arrange
        var image = new Image();

        // Act
        _controller.PostImage(image);

        // Assert
        _mockImageService.Verify(service => service.Add(image), Times.Once);
    }

    [Fact]
    public void DeleteImage_CallsDeleteOnImageService()
    {
        // Arrange
        var imageId = 1;

        // Act
        _controller.DeleteImage(imageId);

        // Assert
        _mockImageService.Verify(service => service.Delete(imageId), Times.Once);
    }
}
