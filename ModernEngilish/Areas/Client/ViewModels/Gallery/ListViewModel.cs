using System;

namespace ModernEngilish.Areas.Client.ViewModels.Gallery
{
    public class ListViewModel
    {
        public ListViewModel(int id, string name, string fileNameInSystem)
        {
            Id = id;
            Name = name;
            FileNameInSystem = fileNameInSystem;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FileNameInSystem { get; set; }
    }
}
