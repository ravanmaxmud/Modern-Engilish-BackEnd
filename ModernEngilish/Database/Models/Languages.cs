using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class Languages : BaseEntity , IAuditable
    {
        public string ProgramName { get; set; }
        public string ProgramContent { get; set; }
        public string FileName { get; set; }
        public string FileNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
