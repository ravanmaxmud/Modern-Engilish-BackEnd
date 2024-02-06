using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Languages;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/language")]
    [Authorize(Roles = "admin")]
    public class LanguageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<LanguageController> _logger;

        public LanguageController(DataContext dataContext, IFileService fileService, ILogger<LanguageController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-lang-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.Languages
                  .Select(ep =>
                  new ListViewModel
                  (ep.Id, ep.ProgramName, ep.ProgramContent, _fileService.GetFileUrl(ep.FileNameInSystem, UploadDirectory.Language))).
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

        [HttpGet("add", Name = "admin-lang-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-lang-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Language);

                var languages = new Languages
                {
                    ProgramName = model.Name,
                    ProgramContent = model.Description,
                    FileName = model.PosterImage.FileName,
                    FileNameInSystem = imageNameInSystem
                };
                await _dataContext.Languages.AddAsync(languages);
                await _dataContext.SaveChangesAsync();
                return RedirectToRoute("admin-lang-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Message}) someThings Went Wrong In Adding Proses ");
                return View(model);
            }
        }

        [HttpGet("update/{id}", Name = "admin-lang-update")]
        public async Task<IActionResult> Update([FromRoute] int? id)
        {
            try
            {
                var languages = await _dataContext.Languages.FirstOrDefaultAsync(ep => ep.Id == id);
                if (languages is null) return NotFound("Language Not Found In Db");
                var model = new UpdateViewModel
                {
                    Id = languages.Id,
                    Name = languages.ProgramName,
                    Description = languages.ProgramContent,
                    PosterImgUrl = _fileService.GetFileUrl(languages.FileNameInSystem, UploadDirectory.Language)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Updating Proses ");
                return RedirectToRoute("admin-lang-list");
            }
        }

        [HttpPost("update/{id}", Name = "admin-lang-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            try
            {
                var languages = await _dataContext.Languages.FirstOrDefaultAsync(ep => ep.Id == model.Id);
                if (languages is null) return NotFound("Language Not Found In Db");

                if (model.PosterImage is not null)
                {
                    await _fileService.DeleteAsync(languages.FileNameInSystem, UploadDirectory.Language);
                    var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.Language);
                    languages.FileName = model.PosterImage.FileName;
                    languages.FileNameInSystem = imageNameInSystem;
                }
                languages.ProgramName = model.Name!;
                languages.ProgramContent = model.Description!;
                languages.UpdateAt = DateTime.Now;

                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-lang-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Message.ToString()}) someThings Went Wrong In Updating Proses ");
                return View(model);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-lang-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            try
            {
                var languages = await _dataContext.Languages.FirstOrDefaultAsync(ep => ep.Id == id);
                if (languages is null) return NotFound();
                await _fileService.DeleteAsync(languages.FileNameInSystem, UploadDirectory.Language);
                _dataContext.Languages.Remove(languages);
                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-lang-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Delete Proses ");
                return RedirectToRoute("admin-lang-list");
            }
        }

    }
}
