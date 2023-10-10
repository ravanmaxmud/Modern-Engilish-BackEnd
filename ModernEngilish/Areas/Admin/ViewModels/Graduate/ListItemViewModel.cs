using System.ComponentModel.DataAnnotations;

namespace ModernEngilish.Areas.Admin.ViewModels.Graduate
{
    public class ListItemViewModel
    {
        public ListItemViewModel(string imageUrl, int id)
        {
            ImageUrl = imageUrl;
            Id = id;
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
