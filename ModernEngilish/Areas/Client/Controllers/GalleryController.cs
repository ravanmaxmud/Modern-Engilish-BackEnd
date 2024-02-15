using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModernEngilish.Areas.Client.ViewModels.Gallery;
using ModernEngilish.Contracts.File;
using ModernEngilish.Contracts.Gallery;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Client.Controllers
{
    [Area("client")]
    [Route("gallery")]
    public class GalleryController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly ILogger<GalleryController> _logger;

        public GalleryController(DataContext dbContext, IFileService fileService, ILogger<GalleryController> logger)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("index",Name ="gallery-index")]
        public async Task<IActionResult> Index()
        {
            var model = await _dbContext.Galleries.Where(g=> g.GalleryName == GalleryName.Parties)
            .Select(g => new ListViewModel(g.Id, g.GalleryName.ToString(),
            _fileService.GetFileUrl(g.FileNameInSystem, UploadDirectory.Gallery))).ToListAsync();
            
            return View(model);
        }
        [HttpGet("graduates",Name ="gallery-graduates")]
        public async Task<IActionResult> Graduates()
        {
            var model = await _dbContext.Galleries.Where(g=> g.GalleryName == GalleryName.Graduates)
            .Select(g => new ListViewModel(g.Id, g.GalleryName.ToString(),
            _fileService.GetFileUrl(g.FileNameInSystem, UploadDirectory.Gallery))).ToListAsync();
            
            return View(model);
        }
        [HttpGet("handicrafts",Name ="gallery-handicrafts")]
        public async Task<IActionResult> Handicrafts()
        {
            var model = await _dbContext.Galleries.Where(g=> g.GalleryName == GalleryName.HandiCrafts).Select(g => new ListViewModel(g.Id, g.GalleryName.ToString(),
            _fileService.GetFileUrl(g.FileNameInSystem, UploadDirectory.Gallery))).ToListAsync();
            
            return View(model);
        }
        [HttpGet("kids",Name ="gallery-kids")]
        public async Task<IActionResult> Kids()
        {
            var model = await _dbContext.Galleries.Where(g=> g.GalleryName == GalleryName.Kids)
            .Select(g => new ListViewModel(g.Id, g.GalleryName.ToString(),
            _fileService.GetFileUrl(g.FileNameInSystem, UploadDirectory.Gallery))).ToListAsync();
            
            return View(model);
        }
    }
}
