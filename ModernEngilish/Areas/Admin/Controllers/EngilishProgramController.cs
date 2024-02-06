using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.EngProgram;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/engilishProgram")]
    [Authorize(Roles = "admin")]
    public class EngilishProgramController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<EngilishProgramController> _logger;

        public EngilishProgramController(DataContext dataContext, IFileService fileService, ILogger<EngilishProgramController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-enPrgoram-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.EngilishPrograms
                  .Select(ep =>
                  new ListViewModel
                  (ep.Id, ep.ProgramName, ep.ProgramContent, _fileService.GetFileUrl(ep.FileNameInSystem, UploadDirectory.EngProgram))).
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

        [HttpGet("add", Name = "admin-enProgram-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-enProgram-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.EngProgram);

                var engProgram = new EngilishProgram
                {
                    ProgramName = model.Name,
                    ProgramContent = model.Description,
                    FileName = model.PosterImage.FileName,
                    FileNameInSystem = imageNameInSystem
                };
                await _dataContext.EngilishPrograms.AddAsync(engProgram);
                await _dataContext.SaveChangesAsync();
                return RedirectToRoute("admin-enPrgoram-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Adding Proses ");
                return View(model);
            }
        }

        [HttpGet("update/{id}", Name = "admin-enProgram-update")]
        public async Task<IActionResult> Update([FromRoute] int? id)
        {
            try
            {
                var enPrgoram = await _dataContext.EngilishPrograms.FirstOrDefaultAsync(ep => ep.Id == id);
                if (enPrgoram is null) return NotFound("Eng Progrdam Not Found In Db");
                var model = new UpdateViewModel
                {
                    Id = enPrgoram.Id,
                    Name = enPrgoram.ProgramName,
                    Description = enPrgoram.ProgramContent,
                    PosterImgUrl = _fileService.GetFileUrl(enPrgoram.FileNameInSystem, UploadDirectory.EngProgram)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something went wrong";
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Updating Proses ");
                return RedirectToRoute("admin-enPrgoram-list");
            }
        }


        [HttpPost("update/{id}", Name = "admin-enProgram-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            try
            {
                var engilish = await _dataContext.EngilishPrograms.FirstOrDefaultAsync(ep => ep.Id == model.Id);
                if (engilish is null) return NotFound("Language Not Found In Db");

                if (model.PosterImage is not null)
                {
                    await _fileService.DeleteAsync(engilish.FileNameInSystem, UploadDirectory.EngProgram);
                    var imageNameInSystem = await _fileService.UploadAsync(model.PosterImage, UploadDirectory.EngProgram);
                    engilish.FileName = model.PosterImage.FileName;
                    engilish.FileNameInSystem = imageNameInSystem;
                }
                engilish.ProgramName = model.Name!;
                engilish.ProgramContent = model.Description!;
                engilish.UpdateAt = DateTime.Now;

                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-enProgram-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Message.ToString()}) someThings Went Wrong In Updating Proses ");
                return View(model);
            }
        }

        [HttpPost("delete/{id}", Name = "admin-enProgram-delete")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            try
            {
                var enPrgoram = await _dataContext.EngilishPrograms.FirstOrDefaultAsync(ep => ep.Id == id);
                if (enPrgoram is null) return NotFound();
                await _fileService.DeleteAsync(enPrgoram.FileNameInSystem, UploadDirectory.EngProgram);
                _dataContext.EngilishPrograms.Remove(enPrgoram);
                await _dataContext.SaveChangesAsync(true);
                return RedirectToRoute("admin-enPrgoram-list");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"({ex.Data}) someThings Went Wrong In Delete Proses ");
                return RedirectToRoute("admin-enPrgoram-list");
            }
        }

    }
}
