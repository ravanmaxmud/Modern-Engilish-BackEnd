using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.EngProgram
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile PosterImage { get; set; }
    }
}
