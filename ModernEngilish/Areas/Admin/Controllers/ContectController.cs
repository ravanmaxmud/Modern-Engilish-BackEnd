using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Contact;
using ModernEngilish.Contracts.Identity;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contact")]
    [Authorize(Roles = RoleNames.ADMIN)]
    public class ContectController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ContectController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-contact-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Contacts.Select(c =>
            new ListViewModel(c.FullName, c.Email, c.PhoneNumber, c.Position, c.Subject, c.Message,
             _fileService.GetFileUrl(c.FileNameInSystem, Contracts.File.UploadDirectory.Contact)
           )).ToListAsync();
            return View(model);
        }
    }
}
