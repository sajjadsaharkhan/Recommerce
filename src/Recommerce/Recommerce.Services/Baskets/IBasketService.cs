using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Baskets.Dto;
using Scrutor.AspNetCore;

namespace Recommerce.Services.Baskets;

public interface IBasketService : IScopedLifetime
{
    public Task<Result<int>> CreateBasketItemAsync(CreateBasketItemInDto basketItemInDto,
        CancellationToken cancellationToken);

    public Task<Result> DeleteBasketItemAsync(string itemIdentifier, CancellationToken cancellationToken);

    public Task<Result<IEnumerable<BasketItemOutDto>>> GetCustomerBasketListAsync(string customerIdentifier,
        CancellationToken cancellationToken);
}