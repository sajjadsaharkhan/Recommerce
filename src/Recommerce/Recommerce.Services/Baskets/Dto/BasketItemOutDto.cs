namespace Recommerce.Services.Baskets.Dto;

public record BasketItemOutDto(
    string UniqueIdentifier,
    string ItemIdentifier,
    string ProductIdentifier,
    int Count
);