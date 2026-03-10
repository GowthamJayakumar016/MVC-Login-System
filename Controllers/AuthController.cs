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

            var token = _authService.Login(dto);

            if (token == null)
            {
                ModelState.AddModelError("Email", "Invalid credentials");
                return View(dto);
            }

            HttpContext.Session.SetString("AuthToken", token);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
                return RedirectToAction("Login");

            return Content("User logged in successfully.");
        }
    }
}