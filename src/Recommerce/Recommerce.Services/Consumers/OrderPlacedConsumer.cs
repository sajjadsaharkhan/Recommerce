using MassTransit;
using Microsoft.Extensions.Logging;
using Recommerce.Services.Events;

namespace Recommerce.Services.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly ILogger<OrderPlacedConsumer> _logger;

    public OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderPlaced> context)
    {
        _logger.LogInformation("Order placed: {OrderId} for customer {CustomerId}", context.Message.OrderUniqueIdentifier, context.Message.CustomerId);
        return Task.CompletedTask;
    }
}
