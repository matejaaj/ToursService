using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;
using Object = Tours.Core.Domain.Entities.Tour.Object;

namespace Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Object> Objects { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }
    public DbSet<Review> Reviews { get; set; }


    public ToursContext(DbContextOptions<ToursContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        ConfigureTour(modelBuilder);
        ConfigureTourExecution(modelBuilder);
        ConfigureCheckpoint(modelBuilder);
        ConfigureObject(modelBuilder);
        ConfigureReview(modelBuilder);
    }

    private void ConfigureObject(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany(t => t.Objects)
                .HasForeignKey(o => o.TourId);

            entity.Property(o => o.Location).HasColumnType("jsonb");
        });
    }

    private void ConfigureCheckpoint(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Checkpoint>(entity =>
        {
            entity.HasOne<Tour>()
                .WithMany(t => t.Checkpoints)
                .HasForeignKey(c => c.TourId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(c => c.Location).HasColumnType("jsonb");


        });
    }
    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasMany<Checkpoint>(t => t.Checkpoints)
                  .WithOne()
                  .HasForeignKey(c => c.TourId);

            entity.HasMany<Object>(t => t.Objects)
                  .WithOne()
                  .HasForeignKey(o => o.TourId);
            entity.HasMany(t => t.Equipment)
                  .WithMany()
                  .UsingEntity(j => j.ToTable("RequiredEquipments"));
            entity.HasMany(t => t.Reviews).WithOne().HasForeignKey(o => o.TourId);
            entity.Property(tour => tour.Durations).HasColumnType("jsonb");
            entity.Property(tour => tour.Price).HasColumnType("jsonb");
            entity.Property(tour => tour.TotalLength).HasColumnType("jsonb");
        });
    }

    private static void ConfigureReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne<Tour>()
                  .WithMany(t => t.Reviews)
                  .HasForeignKey(r => r.TourId);
        });
    }
    private void ConfigureTourExecution(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TourExecution>().Property(item => item.Position).HasColumnType("jsonb");
        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedCheckpoints).HasColumnType("jsonb");
    }
}

