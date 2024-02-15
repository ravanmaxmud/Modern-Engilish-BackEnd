using System;

namespace ModernEngilish.Areas.Admin.ViewModels.Contact
{
    public class ListViewModel
    {
        public ListViewModel(string fullName, string email, string phoneNumber, string position, string subject, string message, string cvFile)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Position = position;
            Subject = subject;
            Message = message;
            CvFile = cvFile;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CvFile { get; set; }
    }
}
