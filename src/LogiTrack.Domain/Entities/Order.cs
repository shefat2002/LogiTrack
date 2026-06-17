using LogiTrack.Domain.Enums;

namespace LogiTrack.Domain.Entities;

public class Order
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string PickupLocation { get; private set; } = string.Empty;
    public string DropoffLocation { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public Guid? AssignedDriverId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    private Order()
    {
    }

    public Order(Guid customerId, string pickupLocation, string dropoffLocation, decimal price)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        PickupLocation = pickupLocation;
        DropoffLocation = dropoffLocation;
        Price = price;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AssignDriver(Guid driverId)
    {
        if(Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot assign driver to an order that is already assigned");
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}