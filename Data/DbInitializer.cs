using NewWebApplication.Models;
using BCrypt.Net;

namespace NewWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Seed Admin User
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "Admin",
                    Email = "admin@bank.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }

            // Seed Credit Scores
            if (!context.CreditScores.Any())
            {
                var creditList = new List<CreditScore>
                {
                    new CreditScore
                    {
                        PAN = "ABCDE12345",
                        FullName = "Gowtham",
                        Score = 720
                    },
                    new CreditScore
                    {
                        PAN = "QWERTY1234",
                        FullName = "Harish Moorthy",
                        Score = 750
                    }
                };

                context.CreditScores.AddRange(creditList);
                context.SaveChanges();
            }
        }
    }
}