using Microsoft.EntityFrameworkCore;
using PlaceViewer.Domain.Models;

namespace PlaceViewer.DatabaseStorage.Infrastructure;

public class PlaceViewerDbContext : DbContext
{
    public PlaceViewerDbContext(DbContextOptions<PlaceViewerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>
        (
            builder =>
            {
                builder.HasKey(p => p.Name);
                builder.Property(p => p.Name).HasMaxLength(100);
                builder.Property(p => p.ImageUrl).HasMaxLength(500);
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}