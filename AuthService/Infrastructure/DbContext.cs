using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace AuthService.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUser>()
            .HasKey(l => l.Username);
    }
    
    public DbSet<AuthUser> AuthUsers { get; set; }
}