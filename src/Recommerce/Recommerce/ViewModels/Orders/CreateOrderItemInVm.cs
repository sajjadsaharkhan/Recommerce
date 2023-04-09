using FluentValidation;
using JetBrains.Annotations;

namespace Recommerce.ViewModels.Orders;

public record CreateOrderItemInVm(
    string ProductUniqueIdentifier,
    int Count,
    int UniquePrice
);

[UsedImplicitly]
public class CreateOrderItemInVmValidator : AbstractValidator<CreateOrderItemInVm>
{
    public CreateOrderItemInVmValidator()
    {
        RuleFor(oi => oi.ProductUniqueIdentifier)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(oi => oi.Count)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
        
        RuleFor(oi => oi.UniquePrice)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}