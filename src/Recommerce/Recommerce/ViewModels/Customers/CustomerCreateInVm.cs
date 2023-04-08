using System;
using FluentValidation;
using JetBrains.Annotations;
using Recommerce.Data.Enums;

namespace Recommerce.ViewModels.Customers;

public record CustomerCreateInVm(
    string UniqueIdentifier,
    DateTime BirthDate,
    GenderType GenderType,
    int ShoppingBalance
);

[UsedImplicitly]
public class CustomerCreateInValidator : AbstractValidator<CustomerCreateInVm>
{
    public CustomerCreateInValidator()
    {
        RuleFor(c => c.UniqueIdentifier)
            .NotNull()
            .NotEmpty();
        RuleFor(c => c.GenderType)
            .IsInEnum();
        RuleFor(c => c.ShoppingBalance)
            .GreaterThanOrEqualTo(0);
    }
}