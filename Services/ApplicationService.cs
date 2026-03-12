using NewWebApplication.Data;
using NewWebApplication.Models;
using NewWebApplication.Services.Interfaces;

namespace NewWebApplication.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public string GenerateApplicationNumber()
        {
            return "APP" + DateTime.Now.Year + "-" + new Random().Next(10000, 99999);
        }

        public List<LoanApplication> GetUserApplications(int userId)
        {
            return _context.LoanApplications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.AppliedDate)
                .ToList();
        }

        public List<LoanApplication> GetApplications(string status)
        {
            var query = _context.LoanApplications.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            return query
                .OrderByDescending(x => x.AppliedDate)
                .ToList();
        }
        public ApplicationDecision GetApplicationDecision(int applicationId)
        {
            return _context.ApplicationDecisions
                .FirstOrDefault(x => x.LoanApplicationId == applicationId);
        }

        public LoanApplication GetApplicationById(int id)
        {
            return _context.LoanApplications
                .FirstOrDefault(x => x.Id == id);
        }

        public void ApproveApplication(int id, string admin, string remark)
        {
            var app = _context.LoanApplications.Find(id);

            if (app == null)
                return;

            app.Status = "Approved";

            var decision = new ApplicationDecision
            {
                LoanApplicationId = id,
                DecisionType = "Approved",
                Remark = remark,
                ActionBy = admin,
                ActionDate = DateTime.Now
            };

            _context.ApplicationDecisions.Add(decision);

            _context.SaveChanges();
        }

        public void RejectApplication(int id, string admin, string reason)
        {
            var app = _context.LoanApplications.Find(id);

            if (app == null)
                return;

            app.Status = "Rejected";

            var decision = new ApplicationDecision
            {
                LoanApplicationId = id,
                DecisionType = "Rejected",
                Remark = reason,
                ActionBy = admin,
                ActionDate = DateTime.Now
            };

            _context.ApplicationDecisions.Add(decision);

            _context.SaveChanges();
        }

        public int GetTotalApplications()
        {
            return _context.LoanApplications.Count();
        }

        public int GetPendingApplications()
        {
            return _context.LoanApplications.Count(x => x.Status == "Pending");
        }

        public int GetApprovedApplications()
        {
            return _context.LoanApplications.Count(x => x.Status == "Approved");
        }

        public int GetRejectedApplications()
        {
            return _context.LoanApplications.Count(x => x.Status == "Rejected");
        }
    }
}