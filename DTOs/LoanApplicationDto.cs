using System.ComponentModel.DataAnnotations;

namespace NewWebApplication.DTOs
{
    public class LoanApplicationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PAN { get; set; }

        [Required]
        public decimal AnnualIncome { get; set; }
    }
}