using ModernEngilish.Contracts.Gallery;
using ModernEngilish.Database.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ModernEngilish.Database.Models
{
    public class Gallery : BaseEntity , IAuditable
    {
        public GalleryName GalleryName { get; set; }
        public string FileName { get; set; }
        public string FileNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
