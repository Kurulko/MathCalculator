using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyCalculator.Models
{
    public class CalculatorContext : IdentityDbContext<User>
    {
        public DbSet<ExpressionAndSolution> ExpressionAndSolution { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<Warning> Warnings { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Word> Words { get; set; }

        public CalculatorContext(DbContextOptions<CalculatorContext> options)
            : base(options)
            => Database.EnsureCreated();
    }
}
