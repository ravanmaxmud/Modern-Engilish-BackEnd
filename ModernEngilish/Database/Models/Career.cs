using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class Career : BaseEntity , IAuditable
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position   { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string FileNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
