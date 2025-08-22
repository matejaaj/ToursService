using Microsoft.EntityFrameworkCore;

namespace Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public ToursContext(DbContextOptions<ToursContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

    }
}

