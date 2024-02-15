using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Client.ViewModels.Home;
using ModernEngilish.Contracts.File;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Client.Controllers
{
    [Area("client")]
    [Route("program")]
    public class ProgramsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ProgramsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "programs-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                EnglishPrograms = await _dbContext.EngilishPrograms.Select(e => new EnglishProgramListViewModel(e.Id, e.ProgramName, e.ProgramContent,
                  _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.EngProgram))).ToListAsync(),

                Courses = await _dbContext.OnlineCourses.Select(e => new OnlineCourses(e.Id, e.ProgramName, e.ProgramContent,
              _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.OnlineCourse))).ToListAsync(),

                Ageds = await _dbContext.Ageds.Select(a => new AgedListItemViewModel(a.Id, a.ProgramName, a.ProgramContent,
                  _fileService.GetFileUrl(a.FileNameInSystem, Contracts.File.UploadDirectory.Aged))).ToListAsync(),

                Languages = await _dbContext.Languages.Select(l => new LanguageListItemViewModel(l.Id, l.ProgramName, l.ProgramContent,
                _fileService.GetFileUrl(l.FileNameInSystem, Contracts.File.UploadDirectory.Language))).ToListAsync(),
            };
            return View(model);
        }

        [HttpGet("languages", Name = "programs-languages")]
        public async Task<IActionResult> Languages()
        {
            var model = new IndexViewModel
            {
                EnglishPrograms = await _dbContext.EngilishPrograms.Select(e => new EnglishProgramListViewModel(e.Id, e.ProgramName, e.ProgramContent,
                  _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.EngProgram))).ToListAsync(),

                Courses = await _dbContext.OnlineCourses.Select(e => new OnlineCourses(e.Id, e.ProgramName, e.ProgramContent,
              _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.OnlineCourse))).ToListAsync(),

                Ageds = await _dbContext.Ageds.Select(a => new AgedListItemViewModel(a.Id, a.ProgramName, a.ProgramContent,
                  _fileService.GetFileUrl(a.FileNameInSystem, Contracts.File.UploadDirectory.Aged))).ToListAsync(),

                Languages = await _dbContext.Languages.Select(l => new LanguageListItemViewModel(l.Id, l.ProgramName, l.ProgramContent,
                _fileService.GetFileUrl(l.FileNameInSystem, Contracts.File.UploadDirectory.Language))).ToListAsync(),
            };
            return View(model);
        }

        [HttpGet("ageds", Name = "programs-ageds")]
        public async Task<IActionResult> Ageds()
        {
            var model = new IndexViewModel
            {
                EnglishPrograms = await _dbContext.EngilishPrograms.Select(e => new EnglishProgramListViewModel(e.Id, e.ProgramName, e.ProgramContent,
                  _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.EngProgram))).ToListAsync(),

                Courses = await _dbContext.OnlineCourses.Select(e => new OnlineCourses(e.Id, e.ProgramName, e.ProgramContent,
              _fileService.GetFileUrl(e.FileNameInSystem, Contracts.File.UploadDirectory.OnlineCourse))).ToListAsync(),

                Ageds = await _dbContext.Ageds.Select(a => new AgedListItemViewModel(a.Id, a.ProgramName, a.ProgramContent,
                  _fileService.GetFileUrl(a.FileNameInSystem, Contracts.File.UploadDirectory.Aged))).ToListAsync(),

                Languages = await _dbContext.Languages.Select(l => new LanguageListItemViewModel(l.Id, l.ProgramName, l.ProgramContent,
                _fileService.GetFileUrl(l.FileNameInSystem, Contracts.File.UploadDirectory.Language))).ToListAsync(),
            };
            return View(model);
        }
    }
}
