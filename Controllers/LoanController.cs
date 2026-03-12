using Microsoft.AspNetCore.Mvc;
using NewWebApplication.DTOs;
using NewWebApplication.Models;
using NewWebApplication.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NewWebApplication.Services.Interfaces;

namespace NewWebApplication.Controllers
{
    public class LoanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IApplicationService _service;

        public LoanController(AppDbContext context, IApplicationService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Apply(LoanApplicationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            int age = DateTime.Now.Year - dto.DateOfBirth.Year;

            if (age < 21)
            {
                ModelState.AddModelError("", "You must be at least 21 years old.");
                return View(dto);
            }

            if (dto.AnnualIncome < 150000)
            {
                ModelState.AddModelError("", "Income must be greater than ₹150000.");
                return View(dto);
            }

            var credit = _context.CreditScores
                .FirstOrDefault(x => x.PAN == dto.PAN);

            if (credit == null)
            {
                ModelState.AddModelError("", "PAN not found in credit bureau.");
                return View(dto);
            }

            int score = credit.Score;

            decimal creditLimit = dto.AnnualIncome * 0.4m;

            ViewBag.Name = dto.FirstName + " " + dto.LastName;
            ViewBag.FirstName = dto.FirstName;
            ViewBag.LastName = dto.LastName;
            ViewBag.PAN = dto.PAN;
            ViewBag.AnnualIncome = dto.AnnualIncome;
            ViewBag.CreditScore = score;
            ViewBag.CreditLimit = creditLimit;

            return View("CreditResult");
        }

        [HttpPost]
        public IActionResult ConfirmLoan(string FirstName, string LastName, string PAN,
            decimal AnnualIncome, int CreditScore, decimal CreditLimit)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var userId = int.Parse(jwt.Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var applicationNumber = _service.GenerateApplicationNumber();

            var loan = new LoanApplication
            {
                FirstName = FirstName,
                LastName = LastName,
                PAN = PAN,
                AnnualIncome = AnnualIncome,
                CreditScore = CreditScore,
                CreditLimit = CreditLimit,
                Status = "Pending",
                AppliedDate = DateTime.Now,
                UserId = userId,
                ApplicationNumber = applicationNumber
            };

            _context.LoanApplications.Add(loan);
            _context.SaveChanges();

            ViewBag.ApplicationNumber = applicationNumber;
            ViewBag.Success = true;

            return View("CreditResult");
        }

        public IActionResult ViewApplications()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var userId = int.Parse(jwt.Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var applications = _service.GetUserApplications(userId);

            return View(applications);
        }

        public IActionResult ViewRemark(int id)
        {
            var application = _service.GetApplicationById(id);

            var decision = _service.GetApplicationDecision(id);

            ViewBag.Decision = decision;

            return View(application);
        }
    }
}