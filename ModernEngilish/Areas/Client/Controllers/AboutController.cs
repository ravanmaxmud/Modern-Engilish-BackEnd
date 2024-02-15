using System;
using Microsoft.AspNetCore.Mvc;

namespace ModernEngilish.Areas.Client.Controllers
{
    [Area("client")]
    [Route("about")]
    public class AboutController : Controller
    {
        [HttpGet("index",Name ="about-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
