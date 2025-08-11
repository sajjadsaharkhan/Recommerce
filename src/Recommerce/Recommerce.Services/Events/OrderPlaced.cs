namespace Recommerce.Services.Events;

public record OrderPlaced(string OrderUniqueIdentifier, int CustomerId);
