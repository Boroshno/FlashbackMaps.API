using FlashbackMaps.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlashbackMaps.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    }

}
