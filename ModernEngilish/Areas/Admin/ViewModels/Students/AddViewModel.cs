using System;
using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.Students
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public IFormFile FileName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
