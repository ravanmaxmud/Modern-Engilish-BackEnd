using Microsoft.AspNetCore.Mvc.Rendering;
using ModernEngilish.Contracts.Gallery;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ModernEngilish.Areas.Admin.ViewModels.Partie
{
    public class AddViewModel
    {
        [Required]
        public GalleryName GalleryName { get; set; }
        [Required]
        public IFormFile PosterImage { get; set; }
    }
}
