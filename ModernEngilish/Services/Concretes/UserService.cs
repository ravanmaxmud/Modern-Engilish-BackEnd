using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Contracts.Identity;
using ModernEngilish.Database;
using ModernEngilish.Database.Models;
using ModernEngilish.Exceptions;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Services.Concretes
{
    public class UserService : IUserService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _currentUser;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }
                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(u => u.Type == CustomClaimNames.ID);
                if (idClaim is null)
                {
                    throw new IdentityCookieException("Identity cookie not found!");
                }
                _currentUser = _context.Users.First(u => u.Id == int.Parse(idClaim.Value));

                return _currentUser;
            }

        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var model = await _context.Users.FirstOrDefaultAsync(u => u.Mail == email);
            if (model is null || model.Password != password)
            {
                return false;
            }
            return true;
        }

        public string GetCurrentUserFullName()
        {
            throw new NotImplementedException();
        }

        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID,id.ToString())
            };
            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _context.Users.FirstAsync(u => u.Mail == email);
            if (user is not null && user.Password == password)
            {
                await SignInAsync(user.Id, role);
            }
        }

        public async Task SignOutAsync()
        {
             await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
