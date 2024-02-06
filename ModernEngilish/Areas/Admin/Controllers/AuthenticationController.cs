using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Areas.Admin.ViewModels.Authentication;
using ModernEngilish.Contracts.Identity;
using ModernEngilish.Database;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(DataContext dbContext, IUserService userService, ILogger<AuthenticationController> logger)
        {
            _dbContext = dbContext;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("login", Name = "auth-login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                if (_userService.IsAuthenticated)
                {
                    return RedirectToRoute("admin-about-index");
                }
                return View(new LoginViewModel());
            }
            catch (System.Exception)
            {
                return RedirectToRoute("auth-login");
            }
        }

        [HttpPost("login", Name = "auth-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
                {
                    ModelState.AddModelError(String.Empty, "Email or password is not correct");
                    _logger.LogWarning($"({model.Email}{model.Password}) This Email and Password  is not correct.");
                    return View(model);
                }
                if (!await _dbContext.Users.AnyAsync(u => u.Mail == model.Email && u.Roles.RoleName == RoleNames.ADMIN))
                {
                    // await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                    return RedirectToRoute("auth-login");
                }
                await _userService.SignInAsync(model!.Email, model!.Password,RoleNames.ADMIN);
                return RedirectToRoute("admin-about-index");
            }
            catch (System.Exception)
            {
               return RedirectToRoute("auth-login");
            }
        }

        [HttpGet("logout", Name = "auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("home-index");
        }
    }
}
