using CaseApp.DataLayer.Abstract;
using CaseApp.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CaseApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userRepository.FindUser(userModel.Username);
            if (user == null)
            {
                ModelState.AddModelError("Username", "User not exists");
                return View(user);
            }

            if (user != null && user.Password != userModel.Password)
            {
                ModelState.AddModelError("Password", "Wrong password");
                return View(user);
            }

            ClaimsIdentity identity = null;
            bool isAuth = false;

            if (userModel.Username == "admin" && userModel.Password == "password")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuth = true;
            }

            if (userModel.Username == "user" && userModel.Password == "password")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuth = true;
            }

            if (isAuth)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
