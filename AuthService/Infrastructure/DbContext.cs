using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace AuthService.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>()
            .HasKey(l => l.Username);
    }
    
    public DbSet<Login> Logins { get; set; }
}