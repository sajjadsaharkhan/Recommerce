using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recommerce.Data.DbContexts;
using Recommerce.Services.Events;

namespace Recommerce.Services.Consumers;

public class ProductRatedConsumer : IConsumer<ProductRated>
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ProductRatedConsumer> _logger;

    public ProductRatedConsumer(AppDbContext dbContext, ILogger<ProductRatedConsumer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductRated> context)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.UniqueIdentifier == context.Message.ProductUniqueIdentifier);

        if (product != null)
        {
            product.CommentCount = (product.CommentCount ?? 0) + 1;
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Product rated: {ProductId}", context.Message.ProductUniqueIdentifier);
        }
    }
}
