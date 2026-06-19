using MediatR;

namespace LogiTrack.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId, 
    string PickupLocation, 
    string DropoffLocation, 
    decimal Price) : IRequest<Guid>;