using FluentValidation;

namespace LogiTrack.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(o => o.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");
        
        RuleFor(o => o.PickupLocation)
            .NotEmpty().WithMessage("Pickup location is required.")
            .MaximumLength(200).WithMessage("Pickup location cannot exceed 200 characters.");
        
        RuleFor(o => o.DropoffLocation)
            .NotEmpty().WithMessage("Dropoff location is required.")
            .MaximumLength(200).WithMessage("Dropoff location cannot exceed 200 characters.");
        
        RuleFor(o => o.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}