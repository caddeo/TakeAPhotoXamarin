using System.Data.Entity;

namespace ImagesWebApi.Models
{
    public class ImageContext : DbContext
    {
        public DbSet<SavedImage> Images { get; set; }
    }
}
