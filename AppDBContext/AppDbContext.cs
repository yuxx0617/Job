using Job.Model;
using Microsoft.EntityFrameworkCore;

namespace Job.AppDBContext;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<UserModel> User { get; set; }
    public DbSet<TestModel> Test { get; set; }
    public DbSet<SeletionModel> Seletion { get; set; }
    public DbSet<UserAnswerModel> UserAnswer { get; set; }
}