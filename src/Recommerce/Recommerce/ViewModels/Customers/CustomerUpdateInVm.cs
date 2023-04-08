using System;
using FluentValidation;
using Recommerce.Data.Enums;

namespace Recommerce.ViewModels.Customers;

public record CustomerUpdateInVm(
    DateTime? BirthDate,
    GenderType? GenderType,
    int? ShoppingBalance
);

public class CustomerUpdateInVmValidator : AbstractValidator<CustomerUpdateInVm>
{
    public CustomerUpdateInVmValidator()
    {
        RuleFor(c => c.BirthDate).NotEmpty();
        RuleFor(c => c.GenderType).IsInEnum();
        RuleFor(c => c.ShoppingBalance).GreaterThanOrEqualTo(0);
    }
}