namespace ModernEngilish.Areas.Client.ViewModels.Home
{
    public class EnglishProgramListViewModel
    {
        public EnglishProgramListViewModel(int id, string name, string description, string imageUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
