using System;
using ModernEngilish.Database.Models.Common;

namespace ModernEngilish.Database.Models
{
    public class Role : BaseEntity 
    {
        public string RoleName { get; set; }  
        public List<User>? Users { get; set; }      
    }
}
