using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashbackMaps.Domain
{
    public class Image
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        // URL of the image
        public string ImageUrl { get; set; }

        // Year of the image
        public int Year { get; set; }

        // Reference to the Location
        [ForeignKey("Location")]
        public long LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
