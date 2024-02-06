using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModernEngilish.Areas.Client.ViewModels.Gallery;
using ModernEngilish.Contracts.File;
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
            var model = await _dbContext.Galleries.Select(g => new ListViewModel(g.Id, g.GalleryName.ToString(),
            _fileService.GetFileUrl(g.FileNameInSystem, UploadDirectory.Gallery))).ToListAsync();
            
            return View(model);
        }
    }
}
