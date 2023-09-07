using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.EngProgram
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile? PosterImage { get; set; }

        public string? PosterImgUrl { get; set; }

    }
}
