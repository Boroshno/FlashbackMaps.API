using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;
using FlashbackMaps.Domain;
using System.Net;

public class ImagesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ImagesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("User-Agent", "IntegrationTest");
    }

    [Fact]
    public async Task GetImageByLocationId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var location = new Location(44, 55);
        var json = JsonConvert.SerializeObject(location);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Create a new location
        var createResponse = await _client.PostAsync("/locations", content);
        createResponse.EnsureSuccessStatusCode();

        // Request all locations to find the new location ID
        var getAllResponse = await _client.GetAsync("/locations");
        getAllResponse.EnsureSuccessStatusCode();
        var allLocations = JsonConvert.DeserializeObject<List<Location>>(await getAllResponse.Content.ReadAsStringAsync()) ?? new List<Location>();
        var newLocationId = allLocations.FirstOrDefault(l => l.Latitude == location.Latitude && l.Longitude == location.Longitude)?.Id;

        // Act
        var response = await _client.GetAsync($"/Images/{newLocationId}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task CreateImage_ReturnsSuccessStatusCode()
    {
        // Arrange
        var location = new Location(44, 55);
        var json = JsonConvert.SerializeObject(location);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Create a new location
        var createResponse = await _client.PostAsync("/locations", content);
        createResponse.EnsureSuccessStatusCode();

        // Request all locations to find the new location ID
        var getAllResponse = await _client.GetAsync("/locations");
        getAllResponse.EnsureSuccessStatusCode();
        var allLocations = JsonConvert.DeserializeObject<List<Location>>(await getAllResponse.Content.ReadAsStringAsync()) ?? new List<Location>();
        var newLocationId = allLocations.First(l => l.Latitude == location.Latitude && l.Longitude == location.Longitude).Id;

        var image = new Image
        {
            Name = "TestImage",
            ImageUrl = "https://example.com/testimage.jpg",
            LocationId = newLocationId
        };
        json = JsonConvert.SerializeObject(image);
        content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/Images", content);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task DeleteImage_ReturnsSuccessStatusCode()
    {
        // Arrange
        var imageId = 1;

        // Act
        var response = await _client.DeleteAsync($"/Images/{imageId}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }
}
