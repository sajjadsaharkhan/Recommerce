using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Orders.Dto;
using Scrutor.AspNetCore;

namespace Recommerce.Services.Orders;

public interface IOrderService : IScopedLifetime
{
     public Task<Result> CreateAsync(CreateOrderInDto createOrderInDto, CancellationToken cancellationToken);

     public Task<Result<Dictionary<string, List<string>>>> GetOrderedProductIdentifierAsync(int customerId,
          bool preventDuplication, CancellationToken cancellationToken);
}