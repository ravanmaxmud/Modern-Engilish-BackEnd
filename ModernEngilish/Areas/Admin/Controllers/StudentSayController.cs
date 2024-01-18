using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Students;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/student/say")]
    public class StudentSayController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<StudentSayController> _logger;

        public StudentSayController(DataContext dataContext, IFileService fileService, ILogger<StudentSayController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("List", Name = "admin-student-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.StudentSays
           .Select(s => new
           ListViewModel(s.Id, s.Name + "" + s.Surname,
           _fileService.GetFileUrl(s.FileNameInSystem, UploadDirectory.Student))).ToListAsync();

                return View(model);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"Program({ex.Data}) not found in db ");
                throw;
            }
        }

        [HttpGet("Add", Name = "admin-student-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("Add", Name = "admin-student-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var imageNameInSystem = await _fileService.UploadAsync(model.FileName, UploadDirectory.Student);
            var studentSay = new StudentSay
            {
                Name = model.Name,
                Surname = model.Surname,
                Description = model.Description,
                Position = model.Position,
                FileName = model.FileName.FileName,
                FileNameInSystem = imageNameInSystem
            };
            await _dataContext.StudentSays.AddAsync(studentSay);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-student-list");
        }
        [HttpPost("Delete/{id}", Name = "admin-student-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            try
            {
                var student = await _dataContext.StudentSays.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null) return NotFound();
                await _fileService.DeleteAsync(student.FileNameInSystem, UploadDirectory.Student);
                _dataContext.StudentSays.Remove(student);
                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-student-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Delete Proses ");
                return RedirectToRoute("admin-student-list");
            }
        }
    }
}
