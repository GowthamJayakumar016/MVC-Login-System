using Microsoft.AspNetCore.Mvc;
using NewWebApplication.Services.Interfaces;

namespace NewWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationService _service;

        public AdminController(IApplicationService service)
        {
            _service = service;
        }

        public IActionResult Index(string status)
        {
            var applications = _service.GetApplications(status);

            return View(applications);
        }

        public IActionResult Details(int id)
        {
            var app = _service.GetApplicationById(id);

            return View(app);
        }

        [HttpPost]
        public IActionResult Approve(int id, string adminName, string remark)
        {
            _service.ApproveApplication(id, adminName, remark);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id, string adminName, string reason)
        {
            _service.RejectApplication(id, adminName, reason);

            return RedirectToAction("Index");
        }
    }
}