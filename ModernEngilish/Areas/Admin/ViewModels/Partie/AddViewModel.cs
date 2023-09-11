using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.Partie
{
    public class AddViewModel
    {
        [Required]
        public IFormFile PosterImage { get; set; }
    }
}
