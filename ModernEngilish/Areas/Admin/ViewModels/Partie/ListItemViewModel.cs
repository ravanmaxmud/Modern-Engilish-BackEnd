using ModernEngilish.Contracts.Gallery;

namespace ModernEngilish.Areas.Admin.ViewModels.Partie
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string imageUrl, GalleryName galleryName)
        {
            Id = id;
            ImageUrl = imageUrl;
            GalleryName = galleryName;
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public GalleryName GalleryName { get; set; }
    }
}
