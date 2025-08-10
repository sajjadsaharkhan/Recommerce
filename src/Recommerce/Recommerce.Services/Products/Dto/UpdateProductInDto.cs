namespace Recommerce.Services.Products.Dto;

public record UpdateProductInDto(
    string Name,
    int? BrandId,
    string Size,
    string Color,
    string? Embedding,
    int? WeightInKg,
    float? ReviewRate,
    int Price
);