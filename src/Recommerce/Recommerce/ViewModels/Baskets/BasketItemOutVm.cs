namespace Recommerce.ViewModels.Baskets;

public record BasketItemOutVm(
    string UniqueIdentifier,
    string ItemIdentifier,
    string ProductIdentifier,
    int Count
);