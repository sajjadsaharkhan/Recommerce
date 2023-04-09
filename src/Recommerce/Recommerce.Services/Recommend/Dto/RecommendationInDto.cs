namespace Recommerce.Services.Recommend.Dto;

public record RecommendationInDto(
    string CustomerIdentifier,
    int ProductCount,
    bool PreventRepetitiveProducts,
    byte AccuracyPercentage
);