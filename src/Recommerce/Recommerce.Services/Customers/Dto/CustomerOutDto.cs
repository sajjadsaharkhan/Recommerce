using Recommerce.Data.Enums;

namespace Recommerce.Services.Customers.Dto;

public record CustomerOutDto(
    string UniqueIdentifier,
    DateTime BirthDate,
    GenderType GenderType,
    int ShoppingBalance,
    DateTime RegisterDate,
    DateTime LastLoginDate
);