using Microsoft.AspNetCore.Mvc;
using NewWebApplication.DTOs;
using NewWebApplication.Services.Interfaces;

namespace NewWebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = _authService.Register(dto);

            if (!result)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = _authService.Login(dto);

            if (result == null)
            {
                ModelState.AddModelError("Email", "Invalid credentials");
                return View(dto);
            }

            HttpContext.Session.SetString("AuthToken", result.Token);
            HttpContext.Session.SetString("UserRole", result.Role);

            if (result.Role == "Admin")
                return RedirectToAction("AdminDashboard", "Dashboard");

            return RedirectToAction("UserDashboard", "Dashboard");
        }

        
    }
}