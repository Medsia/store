using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using Store.Web.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;

        public AuthController(AuthService authServise)
        {
            this.authService = authServise;
        }
         
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }


        [HttpPost]
        public async Task<IActionResult> LoginValidation(string login, string password)
        {
            login = login.Trim();
            if(!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                var userIsCorrect = await authService.UserIsCorrect(login, password);
                if (userIsCorrect)
                {
                    await Authenticate(login);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View("Login", login);
        }

        public async Task Authenticate(string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
