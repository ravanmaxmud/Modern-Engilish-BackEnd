using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class About : BaseEntity,IAuditable
    {
        public string VidoInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
