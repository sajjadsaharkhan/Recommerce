using System.Collections.Generic;
using FluentValidation;
using JetBrains.Annotations;
using Recommerce.Services.Orders.Dto;

namespace Recommerce.ViewModels.Orders;

public record CreateOrderInVm(
    string OrderUniqueIdentifier,
    string CustomerUniqueIdentifier,
    IEnumerable<CreateOrderItemInVm> OrderItems,
    int? CustomerSessionId,
    int? CustomerLocationId
);

[UsedImplicitly]
public class CreateOrderInVmValidator : AbstractValidator<CreateOrderInVm>
{
    public CreateOrderInVmValidator()
    {
        RuleFor(o => o.CustomerLocationId)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(o => o.CustomerSessionId)
            .GreaterThanOrEqualTo(1);
                
        RuleFor(o => o.OrderUniqueIdentifier)
            .NotEmpty()
            .MaximumLength(50)
            .NotNull();
        
        RuleFor(o => o.CustomerUniqueIdentifier)
            .NotEmpty()
            .MaximumLength(50)
            .NotNull();

        RuleFor(o => o.OrderItems)
            .NotEmpty();
    }
}