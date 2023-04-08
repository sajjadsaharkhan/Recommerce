using FluentValidation;
using JetBrains.Annotations;

namespace Recommerce.ViewModels;

public record PaginationInVm(
    int PageSize,
    int PageNumber
);

[UsedImplicitly]
public class PaginationInVmValidator : AbstractValidator<PaginationInVm>
{
    public PaginationInVmValidator()
    {
        RuleFor(c => c.PageSize)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(c => c.PageNumber)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}