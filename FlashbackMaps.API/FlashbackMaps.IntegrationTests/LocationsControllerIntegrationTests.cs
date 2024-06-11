using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;
using FlashbackMaps.Domain;
using System.Net;

public class LocationsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public LocationsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("User-Agent", "IntegrationTest");
    }

    [Fact]
    public async Task Get_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/locations");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Post_Location_And_Get_ById_ReturnsSuccessStatusCode()
    {
        // Arrange
        var location = new Location(22, 33);
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
        var response = await _client.GetAsync($"/locations/{newLocationId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_Location_ReturnsSuccessStatusCode()
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
        var deleteResponse = await _client.DeleteAsync($"/locations/{newLocationId}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_DeletedLocation_ReturnsNotFoundStatusCode()
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

        // Delete the location
        var deleteResponse = await _client.DeleteAsync($"/locations/{newLocationId}");
        deleteResponse.EnsureSuccessStatusCode();

        // Act
        var response = await _client.GetAsync($"/locations/{newLocationId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
