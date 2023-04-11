namespace Recommerce.Services.Baskets.Dto;

public record CreateBasketItemInDto(
    string UniqueIdentifier,
    string ItemIdentifier,
    string ProductIdentifier,
    string CustomerIdentifier,
    int Count
);