using Recommerce.Data.Enums;

namespace Recommerce.Services.Customers.Dto;

public record CustomerOutDto(
    Guid UniqueIdentifier,
    DateTime BirthDate,
    GenderType GenderType,
    int ShoppingBalance,
    DateTime RegisterDate,
    DateTime LastLoginDate
);