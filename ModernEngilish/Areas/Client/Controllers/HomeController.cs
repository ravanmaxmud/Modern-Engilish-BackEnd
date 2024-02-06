using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Client.ViewModels.Home;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Client.Controllers
{

    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(DataContext dbContext, IFileService fileService, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("~/", Name = "home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new IndexViewModel
                {
                    EnglishPrograms = await _dbContext.EngilishPrograms.Select(e => new EnglishProgramListViewModel(e.Id, e.ProgramName, e.ProgramContent,
                      _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.EngProgram))).ToListAsync(),

                    Ageds = await _dbContext.Ageds.Select(a => new AgedListItemViewModel(a.Id, a.ProgramName, a.ProgramContent,
                      _fileService.GetFileUrl(a.FileNameInSystem, Contracts.File.UploadDirectory.Aged))).ToListAsync(),

                    Languages = await _dbContext.Languages.Select(l => new LanguageListItemViewModel(l.Id, l.ProgramName, l.ProgramContent,
                    _fileService.GetFileUrl(l.FileNameInSystem, Contracts.File.UploadDirectory.Language))).ToListAsync(),

                    Students = await _dbContext.StudentSays.Select(s => new StudentSayListViewModel(s.Name + "" + s.Surname, s.Position, s.Description,
                    _fileService.GetFileUrl(s.FileNameInSystem, UploadDirectory.Student))).ToListAsync()

                };
                return View(model);
            }
            catch (Exception)
            {
                _logger.LogError("Eror");
                throw;
            }
        }
    }
}
