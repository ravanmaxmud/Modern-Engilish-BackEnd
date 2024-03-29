﻿using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.EngProgram
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        public IFormFile? PosterImage { get; set; }

        public string? PosterImgUrl { get; set; }

    }
}
