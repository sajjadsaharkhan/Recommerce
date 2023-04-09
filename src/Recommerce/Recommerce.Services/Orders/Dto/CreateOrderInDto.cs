namespace Recommerce.Services.Orders.Dto;

public record CreateOrderInDto(
    string CustomerUniqueIdentifier,
    IEnumerable<CreateOrderItemInDto> OrderItems,
    int? CustomerSessionId,
    int? CustomerLocationId
);