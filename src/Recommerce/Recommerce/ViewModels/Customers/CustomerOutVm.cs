using System;
using Recommerce.Data.Enums;

namespace Recommerce.ViewModels.Customers;

public record CustomerOutVm(
    Guid UniqueIdentifier,
    DateTime BirthDate,
    GenderType GenderType,
    int ShoppingBalance,
    DateTime RegisterDate,
    DateTime LastLoginDate
);

