using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Aged;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/aged")]
    public class AgedController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<AgedController> _logger;

        public AgedController(DataContext dataContext, IFileService fileService, ILogger<AgedController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }
        [HttpGet("list", Name = "admin-aged-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.Ageds
                  .Select(ep =>
                  new ListViewModel
                  (ep.Id, ep.ProgramName, ep.ProgramContent, _fileService.GetFileUrl(ep.FileNameInSystem, UploadDirectory.Aged))).
                   ToListAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"Program({ex.Data}) not found in db ");
                throw ex;
            }
        }
        [HttpGet("add", Name = "admin-aged-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-aged-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Aged);

                var aged = new Aged
                {
                    ProgramName = model.Name,
                    ProgramContent = model.Description,
                    FileName = model.PosterImage.FileName,
                    FileNameInSystem = imageNameInSystem
                };
                await _dataContext.Ageds.AddAsync(aged);
                await _dataContext.SaveChangesAsync();
                return RedirectToRoute("admin-aged-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Adding Proses ");
                return View(model);
            }
        }
        [HttpGet("update/{id}", Name = "admin-aged-update")]
        public async Task<IActionResult> Update([FromRoute] int? id)
        {
            try
            {
                var aged = await _dataContext.Ageds.FirstOrDefaultAsync(ep => ep.Id == id);
                if (aged is null) return NotFound("Age Not Found In Db");
                var model = new UpdateViewModel
                {
                    Id = aged.Id,
                    Name = aged.ProgramName,
                    Description = aged.ProgramContent,
                    PosterImgUrl = _fileService.GetFileUrl(aged.FileNameInSystem, UploadDirectory.Aged)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Updating Proses ");
                return RedirectToRoute("admin-aged-list");
            }
        }
        [HttpPost("update/{id}", Name = "admin-aged-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            try
            {
                var aged = await _dataContext.Ageds.FirstOrDefaultAsync(ep => ep.Id == model.Id);
                if (aged is null) return NotFound("Aged Not Found In Db");

                if (model.PosterImage is not null)
                {
                    await _fileService.DeleteAsync(aged.FileNameInSystem, UploadDirectory.Aged);
                    var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Aged);
                    aged.FileName = model.PosterImage.FileName;
                    aged.FileNameInSystem = imageNameInSystem;
                }
                aged.ProgramName = model.Name!;
                aged.ProgramContent = model.Description!;
                aged.UpdateAt = DateTime.Now;

                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-aged-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Message.ToString()}) someThings Went Wrong In Updating Proses ");
                return View(model);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-aged-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            try
            {
                var aged = await _dataContext.Ageds.FirstOrDefaultAsync(ep => ep.Id == id);
                if (aged is null) return NotFound();
                await _fileService.DeleteAsync(aged.FileNameInSystem, UploadDirectory.Aged);
                _dataContext.Ageds.Remove(aged);
                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-aged-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Delete Proses ");
                return RedirectToRoute("admin-aged-list");
            }
        }
    }
}
