namespace Recommerce.Services.Events;

public record ProductRated(string ProductUniqueIdentifier, float? ReviewRate);
