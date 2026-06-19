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
            builder.HasOne<Driver>().WithMany().HasForeignKey(o => o.AssignedDriverId).OnDelete(DeleteBehavior.SetNull);
        });
        
        modelBuilder.Entity<Driver>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(30);
            builder.Property(o => o.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(o => o.Email).HasMaxLength(255);
            builder.Property(o => o.CurrentStatus).IsRequired().HasConversion<string>().HasMaxLength(20);

            builder.HasIndex(o => new { o.Name, o.CreatedAt, o.CurrentStatus });
        });
        
        modelBuilder.Entity<DriverLocation>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Latitude).HasColumnType("decimal(9,6)");
            builder.Property(o => o.Longitude).HasColumnType("decimal(9,6)");
            builder.Property(o => o.Heading).HasColumnType("decimal(5,2)");
            builder.Property(o => o.Timestamp).IsRequired();

            builder.HasOne<Driver>().WithMany().HasForeignKey(o => o.DriverId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<OrderTrackingLog>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.PreviousStatus).HasConversion<string>().HasMaxLength(20);
            builder.Property(o => o.CurrentStatus).HasConversion<string>().HasMaxLength(20);
            builder.Property(o => o.Notes).HasColumnType("text");
            builder.Property(o => o.Timestamp).IsRequired();

            builder.HasOne<Order>().WithMany().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<OutboxMessage>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.MessageType).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Payload).IsRequired().HasColumnType("jsonb"); 
            builder.Property(o => o.OccurredOn).IsRequired();
            builder.Property(o => o.ProcessedOn);
            builder.Property(o => o.Error).HasColumnType("text");
            
            builder.HasIndex(o => new {o.OccurredOn});
        });
        
    }
    
}