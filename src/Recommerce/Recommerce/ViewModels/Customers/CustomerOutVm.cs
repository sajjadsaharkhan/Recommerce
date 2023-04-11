using System;
using Recommerce.Data.Enums;

namespace Recommerce.ViewModels.Customers;

public record CustomerOutVm(
    string UniqueIdentifier,
    DateTime BirthDate,
    string GenderType,
    int ShoppingBalance,
    DateTime? RegisterDate,
    DateTime? LastLoginDate
);

