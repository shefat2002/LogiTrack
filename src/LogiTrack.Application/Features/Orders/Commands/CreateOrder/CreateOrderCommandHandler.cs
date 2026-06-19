using LogiTrack.Application.Common.Interfaces;
using LogiTrack.Domain.Entities;
using MediatR;

namespace LogiTrack.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(
            request.CustomerId,
            request.PickupLocation,
            request.DropoffLocation,
            request.Price);
        
        _context.Orders.Add(order);
        
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}