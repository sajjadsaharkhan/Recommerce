using MassTransit;
using Microsoft.Extensions.Logging;
using Recommerce.Services.Events;

namespace Recommerce.Services.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation("User created: {UserId} - {UniqueIdentifier}", context.Message.UserId, context.Message.UniqueIdentifier);
        return Task.CompletedTask;
    }
}
