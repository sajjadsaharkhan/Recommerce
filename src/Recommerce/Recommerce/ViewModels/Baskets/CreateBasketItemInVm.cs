using System;
using FluentValidation;
using JetBrains.Annotations;

namespace Recommerce.ViewModels.Baskets;

public record CreateBasketItemInVm(
    string UniqueIdentifier,
    string ItemIdentifier,
    string ProductIdentifier,
    string CustomerIdentifier,
    int Count
);

[UsedImplicitly]
public class CreateBasketItemInVmValidator : AbstractValidator<CreateBasketItemInVm>
{
    public CreateBasketItemInVmValidator()
    {
        var guidLength = Guid.NewGuid().ToString().Length;
        RuleFor(x => x.UniqueIdentifier)
            .NotEmpty()
            .NotNull()
            .MaximumLength(guidLength);
        
        RuleFor(x => x.ItemIdentifier)
            .NotEmpty()
            .NotNull()
            .MaximumLength(guidLength);
        
        RuleFor(x => x.CustomerIdentifier)
            .NotEmpty()
            .NotNull()
            .MaximumLength(guidLength);
        
        RuleFor(x => x.ProductIdentifier)
            .NotEmpty()
            .NotNull()
            .MaximumLength(guidLength);

        RuleFor(x => x.Count)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(1);
    }
}