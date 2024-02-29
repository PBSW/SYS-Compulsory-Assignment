﻿using Microsoft.EntityFrameworkCore;
using TweetService.Domain;

namespace TweetService.Infrastructure;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Setting Primary Keys
        //modelBuilder.Entity<Item>()
        //    .HasKey(i => i.Id)
        //    .HasName("PK_Item");

        //Auto ID generation
        //modelBuilder.Entity<Item>()
        //    .Property(i => i.Id)
        //    .ValueGeneratedOnAdd();
    }
    
    public DbSet<Tweet> ItemTable { get; set; }
}