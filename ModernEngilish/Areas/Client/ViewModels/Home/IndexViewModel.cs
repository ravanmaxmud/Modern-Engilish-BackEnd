namespace ModernEngilish.Areas.Client.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<EnglishProgramListViewModel> EnglishPrograms { get; set; }
        public List<AgedListItemViewModel> Ageds { get; set; }
        public List<LanguageListItemViewModel> Languages { get; set; }
         public List<StudentSayListViewModel> Students { get; set; }
          public List<OnlineCourses> Courses { get; set; }
    }
}
