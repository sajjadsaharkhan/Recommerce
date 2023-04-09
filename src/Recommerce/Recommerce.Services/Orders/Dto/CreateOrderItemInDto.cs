namespace Recommerce.Services.Orders.Dto;

public record CreateOrderItemInDto(
    string ProductUniqueIdentifier,
    int Count,
    int UniquePrice
);