using Moq;
using FlashbackMaps.Data.Repositories;
using FlashbackMaps.Domain;
using Microsoft.EntityFrameworkCore;
using FlashbackMaps.Data;
using System.Linq.Expressions;
using AutoFixture;

public class ImageRepositoryTests
{
    private readonly Fixture _fixture = new Fixture();
    private ImageRepository _repository;
    private readonly Mock<DbSet<Image>> _mockSet;
    private readonly Mock<AppDbContext> _mockContext;

    public ImageRepositoryTests()
    {
        _mockSet = new Mock<DbSet<Image>>();
        _mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        _mockContext.Setup(m => m.Set<Image>()).Returns(_mockSet.Object);
        _repository = new ImageRepository(_mockContext.Object);
    }

    [Fact]
    public void GetByLocationId_ReturnsExpectedImages()
    {
        // Arrange
        var locationId = 1;
        var images = _fixture.Create<List<Image>>().AsQueryable(); ;
        _mockSet.As<IQueryable<Image>>().Setup(m => m.Provider).Returns(images.AsQueryable().Provider);
        _mockSet.As<IQueryable<Image>>().Setup(m => m.Expression).Returns(images.AsQueryable().Expression);
        _mockSet.As<IQueryable<Image>>().Setup(m => m.ElementType).Returns(images.AsQueryable().ElementType);
        _mockSet.As<IQueryable<Image>>().Setup(m => m.GetEnumerator()).Returns(images.GetEnumerator());
        _mockContext.Setup(c => c.Set<Image>()).Returns(_mockSet.Object);
        _repository = new ImageRepository(_mockContext.Object);

        // Act
        var result = _repository.GetByLocationId(locationId);

        // Assert
        Assert.Equal(images, result.ToList());
    }

    [Fact]
    public void AddImage_CallsAddOnDbSet()
    {
        // Arrange
        var image = new Image();

        // Act
        _repository.AddImage(image);

        // Assert
        _mockSet.Verify(m => m.Add(image), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [Fact]
    public void DeleteImage_CallsRemoveOnDbSet()
    {
        // Arrange
        var imageId = 1L;
        var image = new Image();
        _mockSet.Setup(m => m.Find(imageId)).Returns(image);

        // Act
        _repository.DeleteImage(imageId);

        // Assert
        _mockSet.Verify(m => m.Remove(image), Times.Once);
        _mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
}
