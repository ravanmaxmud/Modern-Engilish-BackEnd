namespace ModernEngilish.Areas.Admin.ViewModels.Career
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string fullName, string email, string phoneNumber, string position, string message, string imageURL)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Position = position;
            Message = message;
            ImageURL = imageURL;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Message { get; set; }
        public string ImageURL { get; set; }
    }
}
