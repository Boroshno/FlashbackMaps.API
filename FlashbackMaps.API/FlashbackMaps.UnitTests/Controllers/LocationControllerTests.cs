using AutoFixture;
using FlashbackMaps.API.Controllers;
using FlashbackMaps.Application.Service;
using FlashbackMaps.Domain;
using Microsoft.Extensions.Logging;
using Moq;

public class LocationsControllerTests
{
    private readonly Mock<ILogger<LocationsController>> _mockLogger;
    private readonly Mock<ILocationsService> _mockLocationsService;
    private readonly Fixture _fixture = new();
    private readonly LocationsController _controller;

    public LocationsControllerTests()
    {
        _mockLogger = new Mock<ILogger<LocationsController>>();
        _mockLocationsService = new Mock<ILocationsService>();
        _controller = new LocationsController(_mockLogger.Object, _mockLocationsService.Object);
    }

    [Fact]
    public void Get_ReturnsLocations()
    {
        // Arrange
        var loc1 = _fixture.Create<Location>();
        var loc2 = _fixture.Create<Location>();
        var locations = new List<Location> { loc1, loc2 };
        _mockLocationsService.Setup(service => service.GetLocations()).Returns(locations);

        // Act
        var result = _controller.Get();

        // Assert
        Assert.Equal(locations, result);
    }

    [Fact]
    public void Get_ById_ReturnsLocation()
    {
        // Arrange
        var location = _fixture.Create<Location>();
        _mockLocationsService.Setup(service => service.GetLocations(It.IsAny<long>())).Returns(location);

        // Act
        var result = _controller.Get(1);

        // Assert
        Assert.Equal(location, result);
    }

    [Fact]
    public void Post_AddsLocation()
    {
        // Arrange
        var location = _fixture.Create<Location>();

        // Act
        _controller.Post(location);

        // Assert
        _mockLocationsService.Verify(service => service.AddLocation(location), Times.Once);
    }

    [Fact]
    public void Delete_RemovesLocation()
    {
        // Arrange
        var locationId = 1;

        // Act
        _controller.Delete(locationId);

        // Assert
        _mockLocationsService.Verify(service => service.DeleteLocation(locationId), Times.Once);
    }
}
