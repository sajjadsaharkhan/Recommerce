using Recommerce.Data.Enums;

namespace Recommerce.Services.Customers.Dto;

public record CustomerUpdateInDto(
    DateTime? BirthDate,
    GenderType? GenderType,
    int? ShoppingBalance
);