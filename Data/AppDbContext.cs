using Microsoft.EntityFrameworkCore;
using NewWebApplication.Models;

namespace NewWebApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CreditScore>CreditScores { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        
        public DbSet<ApplicationDecision> ApplicationDecisions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>()
                .HasMany(u => u.LoanApplications)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

    
            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.AnnualIncome)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.CreditLimit)
                .HasPrecision(18, 2);
          
            modelBuilder.Entity<ApplicationDecision>()
                .HasOne(d => d.LoanApplication)
                .WithMany(l => l.Decisions)
                .HasForeignKey(d => d.LoanApplicationId);
        
    }
    }
}