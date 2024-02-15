using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Graduate;
using ModernEngilish.Contracts.Identity;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/graduate")]
    [Authorize(Roles =RoleNames.ADMIN)]
    public class GraduateController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<GraduateController> _logger;

        public GraduateController(DataContext dataContext, IFileService fileService, ILogger<GraduateController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-graduate-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.Graduates.Select(g =>
                 new ListItemViewModel(
                        _fileService.GetFileUrl(
                             g.FileNameInSystem, Contracts.File.UploadDirectory.Graduate), g.Id))
                                  .ToListAsync();
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"Program({e.StackTrace}) not found in db ");
                return RedirectToRoute("admin-graduate-list");
            }
        }

        [HttpGet("add", Name = "admin-graduate-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-graduate-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, Contracts.File.UploadDirectory.Graduate);
                var graduate = new Graduate
                {
                    FileName = model.PosterImage.FileName,
                    FileNameInSystem = imageNameInSystem,
                };

                await _dataContext.Graduates.AddAsync(graduate);
                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-graduate-list");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-graduate-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var graduate = await _dataContext.Graduates.FirstOrDefaultAsync(g => g.Id == id);
            if (graduate is null) return NotFound();
            _dataContext.Graduates.Remove(graduate);
            await _dataContext.SaveChangesAsync(true);

            return RedirectToRoute("admin-graduate-list");
        }


    }
}
