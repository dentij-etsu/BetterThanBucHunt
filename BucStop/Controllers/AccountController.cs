using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BucStop.Controllers
{
    public class AccountController : Controller
    {
        public string email { get; set; } = string.Empty;

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email)
        {
            if (Regex.IsMatch(email, @"\b[A-Za-z0-9._%+-]+@etsu\.edu\b"))
            {
                // If authentication is successful, create a ClaimsPrincipal and sign in the user
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.NameIdentifier, "user_id"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "custom");
                var userPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user
                await HttpContext.SignInAsync("CustomAuthenticationScheme", userPrincipal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, return to the login page with an error message
                ModelState.AddModelError(string.Empty, "Only ETSU students can play, sorry :(");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CustomAuthenticationScheme");
            return RedirectToAction("Login");
        }


    }
}