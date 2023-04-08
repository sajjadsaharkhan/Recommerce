using Recommerce.Data.Enums;

namespace Recommerce.Services.Customers.Dto;

public record CustomerCreateInDto(
    DateTime BirthDate,
    GenderType GenderType,
    int ShoppingBalance      
);