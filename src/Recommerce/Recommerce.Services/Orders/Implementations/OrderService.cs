using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Customers;
using Recommerce.Services.Orders.Dto;
using Recommerce.Services.Products;

namespace Recommerce.Services.Orders.Implementations;

[UsedImplicitly]
public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;

    public OrderService(AppDbContext dbContext, ICustomerService customerService, IProductService productService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _productService = productService;
    }


    public async Task<Result> CreateAsync(CreateOrderInDto createOrderInDto, CancellationToken cancellationToken)
    {
        var productUniqueIdentifierList = createOrderInDto.OrderItems
            .Select(oi => oi.ProductUniqueIdentifier)
            .ToList();
        var productIdDictionaryResult =
            await _productService.GetProductIdAsync(productUniqueIdentifierList, cancellationToken);
        if (productIdDictionaryResult.IsFailed)
            return productIdDictionaryResult;

        var customerIdResult = await _customerService.GetCustomerIdAsync(createOrderInDto.CustomerUniqueIdentifier,
            cancellationToken);
        if (customerIdResult.IsFailed)
            return customerIdResult;

        var orderEntityList = createOrderInDto.OrderItems
            .Select(oi => new Order
            {
                OrderUniqueIdentifier = createOrderInDto.OrderUniqueIdentifier,
                CustomerId = customerIdResult.Data,
                CustomerLocationId = createOrderInDto.CustomerLocationId,
                CustomerSessionId = createOrderInDto.CustomerSessionId,
                ProductId = productIdDictionaryResult.Data.GetValueOrDefault(oi.ProductUniqueIdentifier),
                Count = oi.Count,
                UniquePrice = oi.UniquePrice
            });

        await _dbContext.Orders.AddRangeAsync(orderEntityList, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<Dictionary<string, List<string>>>> GetOrderedProductIdentifierAsync(int customerId,
        bool preventDuplication, CancellationToken cancellationToken)
    {
        var productIdList = await (from order in _dbContext.Orders
            join product in _dbContext.Products
                on order.ProductId equals product.Id
            where !preventDuplication || order.CustomerId == customerId
            select new
            {
                ProductUniqueIdentifier = product.UniqueIdentifier,
                order.OrderUniqueIdentifier
            }).ToListAsync(cancellationToken);

        var productGrouping = productIdList.GroupBy(x => x.OrderUniqueIdentifier)
            .ToDictionary(keySelector => keySelector.Key,
                valueSelector => valueSelector.Select(x => x.ProductUniqueIdentifier).ToList());

        return productGrouping;
    }
}