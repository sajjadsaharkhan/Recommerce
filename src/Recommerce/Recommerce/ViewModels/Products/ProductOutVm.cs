namespace Recommerce.ViewModels.Products;

public record ProductOutVm(
    string UniqueIdentifier,
    string Name,
    int? BrandId,
    string Size,
    string Color,
    int? WeightInKg,
    float? ReviewRate,
    int? CommentCount,
    int Price
);