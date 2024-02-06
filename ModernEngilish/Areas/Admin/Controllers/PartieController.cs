using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Partie;
using ModernEngilish.Contracts.File;
using ModernEngilish.Contracts.Gallery;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;
using System.Linq;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/partie")]
    [Authorize(Roles = "admin")]
    public class PartieController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<PartieController> _logger;

        public PartieController(DataContext dataContext, IFileService fileService, ILogger<PartieController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-partie-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.Galleries
                        .Select(P =>
                        new ListItemViewModel
                        (P.Id, _fileService.GetFileUrl(P.FileNameInSystem, UploadDirectory.Gallery), P.GalleryName)).ToListAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) not found in db ");
                throw ex;
            }
        }

        [HttpGet("add", Name = "admin-partie-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-partie-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Gallery);
                var partie = new Gallery
                {
                    GalleryName = model.GalleryName,
                    FileName = model.PosterImage.FileName,
                    FileNameInSystem = imageNameInSystem
                };
                await _dataContext.Galleries.AddAsync(partie);
                await _dataContext.SaveChangesAsync();
                return RedirectToRoute("admin-partie-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Adding Proses ");
                return View(model);
            }
        }
        [HttpPost("delete/{id}", Name = "admin-partie-delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var partie = await _dataContext.Galleries.FirstOrDefaultAsync(p => p.Id == id);
            if (partie is null) return NotFound("Gallery Not Found In db");
            await _fileService.DeleteAsync(partie.FileNameInSystem, UploadDirectory.Gallery);
            _dataContext.Galleries.Remove(partie);
            await _dataContext.SaveChangesAsync(true);
            return RedirectToRoute("admin-partie-list");
        }

    }
}
