using System;
using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class StudentSay : BaseEntity ,IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position  { get; set; }
        public string FileName { get; set; }
        public string FileNameInSystem { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
