using System;
using Microsoft.AspNetCore.Mvc;
using ModernEngilish.Areas.Admin.ViewModels.Contact;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ContactController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "contact-index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("send", Name = "contact-send")]
        public async Task<IActionResult> Send(SendViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToRoute("contact-index");
            var fileNameInFileSystem = await _fileService.UploadAsync(model.CvFile, UploadDirectory.Contact);
            var contact = new Contact
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Position = model.Position,
                Subject = model.Subject,
                Message = model.Message,
                FileName = model.CvFile.FileName,
                FileNameInSystem = fileNameInFileSystem
            };
             await _dbContext.Contacts.AddAsync(contact);
             await _dbContext.SaveChangesAsync();
            return RedirectToRoute("contact-index");
        }
    }
}
