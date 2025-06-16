using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // 1. Sign out cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Clear session
            HttpContext.Session.Clear();

            // 3. Remove all cookies explicitly (including auth cookie)
            foreach (string cookie in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(cookie);
            }

            // 4. Clear current user principal
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // 5. Add headers to prevent caching (optional but recommended)
            HttpContext.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            HttpContext.Response.Headers.Pragma = "no-cache";
            HttpContext.Response.Headers.Expires = "0";

            // 6. Redirect to login page with a query param to avoid cached pages on back button
            return Redirect("/auth/login?loggedOut=true");
        }


        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input.";
                return View(loginDto);
            }

            ApiResponse<string> response = await authService.LoginAsync(loginDto);

            if (response.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = response.Message;
            return View(loginDto);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input.";
                return View(registerDto);
            }

            ApiResponse<string> response = await authService.RegisterAsync(registerDto);
            if (response.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = response.Message;
            return View(registerDto);
        }
    }
}