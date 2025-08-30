using Microsoft.EntityFrameworkCore;
using Tours.Core.Domain.Entities;
namespace Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        ConfigureTour(modelBuilder);
        ConfigureCheckpoint(modelBuilder);
    }
    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>().Property(u => u.Difficulty).HasConversion<string>();
        modelBuilder.Entity<Tour>().Property(u => u.Status).HasConversion<string>();
    }
    private static void ConfigureCheckpoint(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Checkpoint>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany()
                .HasForeignKey(c => c.TourId);
        });
    }
}

