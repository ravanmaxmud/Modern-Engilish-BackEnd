using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Career;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/career")]
    [Authorize(Roles = "admin")]
    public class CareerController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<CareerController> _logger;

        public CareerController(DataContext dataContext, IFileService fileService, ILogger<CareerController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-career-list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var model = await _dataContext.Careers
               .Select(c =>
               new ListItemViewModel(c.Id, c.FullName, c.Email, c.PhoneNumber, c.Position, c.Message,
               _fileService.GetFileUrl(c.FileNameInSystem, Contracts.File.UploadDirectory.Career))).ToListAsync();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                _logger.LogWarning($"Program({ex.Data}) not found in db ");
                throw;
            }
        }
    }
}
