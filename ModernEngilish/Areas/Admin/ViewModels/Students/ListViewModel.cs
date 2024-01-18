using System;

namespace ModernEngilish.Areas.Admin.ViewModels.Students
{
    public class ListViewModel
    {
        public ListViewModel(int id, string fullName, string imageNameInSystem)
        {
            Id = id;
            FullName = fullName;
            ImageNameInSystem = imageNameInSystem;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageNameInSystem { get; set; }
    }
}
