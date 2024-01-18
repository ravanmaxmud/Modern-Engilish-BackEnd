using System;

namespace ModernEngilish.Areas.Client.ViewModels.Home
{
    public class StudentSayListViewModel
    {
        public StudentSayListViewModel(string fullName, string position, string description, string imageNameInSystem)
        {
            FullName = fullName;
            Position = position;
            Description = description;
            ImageNameInSystem = imageNameInSystem;
        }

        public string FullName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string ImageNameInSystem { get; set; }
        
    }
}
