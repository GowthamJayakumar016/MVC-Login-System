using NewWebApplication.Models;

public class ApplicationDecision
{
    public int Id { get; set; }

    public int LoanApplicationId { get; set; }

    public string DecisionType { get; set; }

    public string Remark { get; set; }

    public string ActionBy { get; set; }

    public DateTime ActionDate { get; set; }

    public LoanApplication LoanApplication { get; set; }
}