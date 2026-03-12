using NewWebApplication.Models;

namespace NewWebApplication.Services.Interfaces
{
    public interface IApplicationService
    {
        string GenerateApplicationNumber();

        List<LoanApplication> GetUserApplications(int userId);

        List<LoanApplication> GetApplications(string status);

        LoanApplication GetApplicationById(int id);

        void ApproveApplication(int id, string adminName, string remark);

        void RejectApplication(int id, string adminName, string reason);
        ApplicationDecision GetApplicationDecision(int applicationId);
        int GetTotalApplications();

        int GetPendingApplications();

        int GetApprovedApplications();

        int GetRejectedApplications();
    }
}