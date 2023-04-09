using FluentValidation;

namespace Recommerce.ViewModels.Recommend;

public record RecommendationInVm(
    string CustomerIdentifier,
    int ProductCount,
    bool PreventRepetitiveProducts,
    int AccuracyPercentage
);

public class RecommendationInVmValidator : AbstractValidator<RecommendationInVm>
{
    public RecommendationInVmValidator()
    {
        RuleFor(x => x.CustomerIdentifier)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProductCount)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.AccuracyPercentage)
            .NotNull()
            .NotEmpty()
            .InclusiveBetween(1, 100);
    }
}