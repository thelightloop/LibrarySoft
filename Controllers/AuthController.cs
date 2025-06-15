using Library.Common.ApiResponse;
using Library.Common.DTO;
using Library.Services.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : Controller
    {

        // GET: /Auth/Login
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View(); // Renders Login.cshtml
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Optional: remove your custom session/cookie logic here
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login", "Auth");
        }

        // POST: /Auth/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input.";
                return View(loginDto);
            }

            var response = await authService.LoginAsync(loginDto);
            if (!response.Success)
            {
                ViewBag.Error = response.Message;
                return View(loginDto);
            }

            // TODO: Set session or auth cookie as needed
            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(); // Renders Register.cshtml
        }

        // POST: /Auth/Register
        [HttpPost("register")]
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
                return RedirectToAction("index","Home");
            }

            ViewBag.Error = response.Message;
            return View(registerDto);

        }
    }
}