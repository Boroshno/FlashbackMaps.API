using AutoFixture;
using FlashbackMaps.Application.Service;
using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;
using Moq;

public class LocationsServiceTests
{
    private readonly Mock<ILocationRepository> _mockLocationRepository;
    private readonly LocationsService _service;
    private readonly IFixture _fixture;

    public LocationsServiceTests()
    {
        _mockLocationRepository = new Mock<ILocationRepository>();
        _service = new LocationsService(_mockLocationRepository.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public void AddLocation_CallsRepository()
    {
        // Arrange
        var location = _fixture.Create<Location>();

        // Act
        _service.AddLocation(location);

        // Assert
        _mockLocationRepository.Verify(repo => repo.Add(location), Times.Once);
    }

    [Fact]
    public void GetLocations_ReturnsAllLocations()
    {
        // Arrange
        var locations = _fixture.Create<List<Location>>();
        _mockLocationRepository.Setup(repo => repo.GetAll()).Returns(locations);

        // Act
        var result = _service.GetLocations();

        // Assert
        Assert.Equal(locations, result);
    }

    [Fact]
    public void GetLocations_ById_ReturnsLocation()
    {
        // Arrange
        var location = _fixture.Create<Location>();
        _mockLocationRepository.Setup(repo => repo.GetById(It.IsAny<long>())).Returns(location);

        // Act
        var result = _service.GetLocations(1);

        // Assert
        Assert.Equal(location, result);
    }

    [Fact]
    public void DeleteLocation_CallsRepository()
    {
        // Arrange
        var locationId = 1;

        // Act
        _service.DeleteLocation(locationId);

        // Assert
        _mockLocationRepository.Verify(repo => repo.Delete(locationId), Times.Once);
    }
}
