using System;
using dotenv.net;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Models;

public partial class CdnMainContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public CdnMainContext()
    {
    }
    
    public CdnMainContext(DbContextOptions<CdnMainContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
