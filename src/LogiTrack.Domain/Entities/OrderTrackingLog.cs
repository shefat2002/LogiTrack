using LogiTrack.Domain.Enums;

namespace LogiTrack.Domain.Entities;

public class OrderTrackingLog
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public OrderStatus? PreviousStatus { get; private set; }
    public OrderStatus? CurrentStatus { get; private set; }
    public DateTime Timestamp { get; init; }
    public string? Notes { get; private set; }

    private OrderTrackingLog()
    {
    }

    public OrderTrackingLog(Guid orderId, OrderStatus? previousStatus, OrderStatus? currentStatus, string? notes = null)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        PreviousStatus = previousStatus;
        CurrentStatus = currentStatus;
        Notes = notes;
        Timestamp = DateTime.UtcNow;
    }
}