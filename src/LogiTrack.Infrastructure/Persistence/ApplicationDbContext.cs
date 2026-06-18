using LogiTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<DriverLocation> DriverLocations => Set<DriverLocation>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<OrderTrackingLog> OrderTrackingLogs => Set<OrderTrackingLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.FullName).IsRequired().HasMaxLength(30);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(o => o.Email).IsUnique();
            builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(20);
            
            builder.HasIndex(o => new { o.FullName, o.CreatedAt });
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.PickupLocation).IsRequired().HasMaxLength(255);
            builder.Property(o => o.DropoffLocation).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Price).HasColumnType("numeric(10,2)");
            builder.Property(o => o.Status).IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.HasIndex(o => new { o.Status, o.CreatedAt });
            
            builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
        });
    }
    
}