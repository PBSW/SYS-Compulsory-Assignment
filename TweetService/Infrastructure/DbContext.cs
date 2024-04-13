using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace TweetService.Infrastructure;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Setting Primary Keys
        modelBuilder.Entity<Tweet>()
            .HasKey(i => i.Id)
            .HasName("PK_Tweet");

        //Auto ID generation
        modelBuilder.Entity<Tweet>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
    }
    
    public DbSet<Tweet> Tweets { get; set; }
}