using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Client.ViewModels.Home;
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

        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("index",Name = "home-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                EnglishPrograms = await _dbContext.EngilishPrograms.Select(e => new EnglishProgramListViewModel(e.Id,e.ProgramName,e.ProgramContent,
                  _fileService.GetFileUrl(e.FileNameInSystem,Contracts.File.UploadDirectory.EngProgram))).ToListAsync(),

                Ageds = await _dbContext.Ageds.Select(a=> new AgedListItemViewModel(a.Id,a.ProgramName,a.ProgramContent,
                  _fileService.GetFileUrl(a.FileNameInSystem,Contracts.File.UploadDirectory.Aged))).ToListAsync(),

                Languages = await _dbContext.Languages.Select(l=> new LanguageListItemViewModel(l.Id,l.ProgramName,l.ProgramContent,
                _fileService.GetFileUrl(l.FileNameInSystem,Contracts.File.UploadDirectory.Language))).ToListAsync(),

            };
            return View(model);
        }
    }
}
