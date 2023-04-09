using FluentValidation;
using JetBrains.Annotations;

namespace Recommerce.ViewModels.Products;

public record UpdateProductInVm(
    string Name,
    int? BrandId,
    string Size,
    string Color,
    int? WeightInKg,
    float? ReviewRate,
    int Price
);

[UsedImplicitly]
public class UpdateProductInVmValidator : AbstractValidator<UpdateProductInVm>
{
    public UpdateProductInVmValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .MaximumLength(500)
            .NotEmpty();
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
        
        RuleFor(p => p.Size)
            .MaximumLength(500);

        RuleFor(p => p.Color)
            .MaximumLength(500);

        RuleFor(p => p.WeightInKg)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(p => p.BrandId)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(p => p.ReviewRate)
            .GreaterThanOrEqualTo(0);
    }
}