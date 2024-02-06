using System;
using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class User :  BaseEntity, IAuditable
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public int? RoleID { get; set; }
        public Role Roles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
