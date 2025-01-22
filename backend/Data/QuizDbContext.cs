using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class QuizDbContext : DbContext
{
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Result> Results { get; set; }

    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Result>().OwnsMany(r => r.Answers);
    }
}
