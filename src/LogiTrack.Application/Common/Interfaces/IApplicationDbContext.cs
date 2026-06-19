using LogiTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<Driver> Drivers { get; }
    DbSet<OrderTrackingLog> OrderTrackingLogs { get; }
    DbSet<DriverLocation> DriverLocations { get; }
    DbSet<OutboxMessage> OutboxMessages { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}