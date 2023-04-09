namespace Recommerce.Services.Orders.Dto;

public record CreateOrderInDto(
    string OrderUniqueIdentifier,
    string CustomerUniqueIdentifier,
    IEnumerable<CreateOrderItemInDto> OrderItems,
    int? CustomerSessionId,
    int? CustomerLocationId
);