using System.ComponentModel.DataAnnotations;

namespace NewWebApplication.Models
{
    public class LoanApplication
    {
        public int Id { get; set; }

        public string ApplicationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PAN { get; set; }

        public decimal AnnualIncome { get; set; }

        public int CreditScore { get; set; }

        public decimal CreditLimit { get; set; }

        public string Status { get; set; }

        public DateTime AppliedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<ApplicationDecision> Decisions { get; set; }
    }
}