using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.Graduate
{
    public class AddViewModel
    {
        [Required]
        public IFormFile PosterImage { get; set; }
    }
}
