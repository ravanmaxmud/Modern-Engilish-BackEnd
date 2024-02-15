using System;
using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.Contact
{
    public class SendViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public IFormFile CvFile { get; set; }
    }
}
