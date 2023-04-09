namespace Recommerce.Services.Products.Dto;

public record CreateProductInDto(
    string UniqueIdentifier,
    string Name,
    int? BrandId,
    string Size,
    string Color,
    int? WeightInKg,
    float? ReviewRate,
    int Price
);