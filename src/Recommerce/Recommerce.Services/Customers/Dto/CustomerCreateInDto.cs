using Recommerce.Data.Enums;

namespace Recommerce.Services.Customers.Dto;

public record CustomerCreateInDto(
    string UniqueIdentifier,
    DateTime? BirthDate,
    GenderType GenderType,
    int ShoppingBalance      
);