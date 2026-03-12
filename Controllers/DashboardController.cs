using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewWebApplication.Data;

namespace NewWebApplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult UserDashboard()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
                return RedirectToAction("Login", "Auth");

            var approvedLoans = _context.LoanApplications
                .Where(x => x.Status == "Approved")
                .ToList();

            return View(approvedLoans);
        }

        public IActionResult AdminDashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Login", "Auth");

            var applications = _context.LoanApplications.ToList();

            return View(applications);
        }
    }
}