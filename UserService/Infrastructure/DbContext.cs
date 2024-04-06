using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Infrastructure;



public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Setting Primary Keys
        modelBuilder.Entity<User>()
            .HasKey(i => i.Id)
            .HasName("PK_Item");

        //Auto ID generation
        modelBuilder.Entity<User>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<User>()
            .HasMany<User>(User => User.Following)
            .WithOne()
            .HasForeignKey(user => user.Id)
            .HasConstraintName("FK_User_Following");
        
        modelBuilder.Entity<User>()
            .HasMany<User>(User => User.Followers)
            .WithOne()
            .HasForeignKey(user => user.Id)
            .HasConstraintName("FK_User_Followers");
        
        //Ensure unique usernames
        modelBuilder.Entity<User>()
            .HasIndex(i => i.Username)
            .IsUnique();
        
        //Ensure unique emails
        modelBuilder.Entity<User>()
            .HasIndex(i => i.Email)
            .IsUnique();
            
    }
    
    public DbSet<User> Users { get; set; }
}