using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernEngilish.Contracts.Identity;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/about")]
    [Authorize(Roles = RoleNames.ADMIN)]
    public class AboutController : Controller
    {
        [HttpGet("index",Name ="admin-about-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
