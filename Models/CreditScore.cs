using System.ComponentModel.DataAnnotations;

namespace NewWebApplication.Models
{
    public class CreditScore
    {
        public int Id { get; set; }

        [Required]
        public string PAN { get; set; }

        public string FullName { get; set; }

        public int Score { get; set; }
    }
}