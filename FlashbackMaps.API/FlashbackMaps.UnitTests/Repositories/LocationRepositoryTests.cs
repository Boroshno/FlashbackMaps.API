using AutoFixture;
using FlashbackMaps.Data;
using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;

public class LocationRepositoryTests
{
    private readonly Mock<DbSet<Location>> _mockSet;
    private readonly Mock<AppDbContext> _mockContext;
    private readonly LocationRepository _repository;
    private readonly IFixture _fixture;

    public LocationRepositoryTests()
    {
        _fixture = new Fixture();
        _mockSet = new Mock<DbSet<Location>>();
        _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        _mockContext.Setup(m => m.Set<Location>()).Returns(_mockSet.Object);
        _repository = new LocationRepository(_mockContext.Object);
    }

    [Fact]
    public void Add_CallsContext()
    {
        // Arrange
        var location = _fixture.Create<Location>();

        // Act
        _repository.Add(location);

        // Assert
        _mockSet.Verify(m => m.Add(location), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAll_ReturnsAllLocations()
    {
        // Arrange
        var locations = _fixture.Create<List<Location>>();
        _mockSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
        _mockSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
        _mockSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
        _mockSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.GetEnumerator());

        // Act
        var result = _repository.GetAll();

        // Assert
        Assert.Equal(locations, result);
    }

    [Fact]
    public void GetById_ReturnsLocation()
    {
        // Arrange
        var location = _fixture.Create<Location>();
        _mockSet.Setup(m => m.Find(It.IsAny<long>())).Returns(location);

        // Act
        var result = _repository.GetById(1);

        // Assert
        Assert.Equal(location, result);
    }
}
