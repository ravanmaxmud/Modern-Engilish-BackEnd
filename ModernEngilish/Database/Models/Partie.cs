using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class Partie : BaseEntity , IAuditable
    {
        public string FileName { get; set; }
        public string FileNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
