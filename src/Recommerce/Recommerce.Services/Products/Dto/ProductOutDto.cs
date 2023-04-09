namespace Recommerce.Services.Products.Dto;

public record ProductOutDto(
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