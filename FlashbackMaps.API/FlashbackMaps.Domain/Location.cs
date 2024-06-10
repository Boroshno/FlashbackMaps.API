using System.ComponentModel.DataAnnotations;

namespace FlashbackMaps.Domain
{
    public class Location
    {
        [Key]
        public long Id { get; set; }
        // Latitude in decimal degrees
        public double Latitude { get; set; }

        // Longitude in decimal degrees
        public double Longitude { get; set; }

        // Address as a readable string
        public string Address { get; set; }

        // Optional description or notes about the location
        public string Description { get; set; }

        // Constructor to initialize a new Location
        public Location(double latitude, double longitude, string address = "", string description = "")
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Description = description;
        }

        // Override ToString for easy debugging and display
        public override string ToString()
        {
            return $"Latitude: {Latitude}, Longitude: {Longitude}, Address: {Address}, Description: {Description}";
        }
    }
}
