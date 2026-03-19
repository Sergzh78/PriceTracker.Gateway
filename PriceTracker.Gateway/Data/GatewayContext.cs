using Microsoft.EntityFrameworkCore;
using PriceTracker.Gateway.Entities;

namespace PriceTracker.Gateway.Data;

public class GatewayDbContext : DbContext
{
    public GatewayDbContext(DbContextOptions<GatewayDbContext> options)
        : base(options)
    {
    }

    public DbSet<SimpleRegistrationUser> SimpleRegistrationUser { get; set; }   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimpleRegistrationUser>(entity =>
        {
            entity.ToTable("SimpleRegistrationUser");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
        
    }
}