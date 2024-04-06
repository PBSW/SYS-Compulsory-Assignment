using Microsoft.EntityFrameworkCore;
using TweetService.Domain;

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
        
        modelBuilder.Entity<Tweet>()
            .HasMany(i => i.Replies)
            .WithOne()
            .HasForeignKey(i => i.ReplyTo)
            .HasConstraintName("FK_Tweet_Replies");

        modelBuilder.Entity<Tweet>()
            .Property(t => t.Likes)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList()
            );
    }
    
    public DbSet<Tweet> Tweets { get; set; }
}