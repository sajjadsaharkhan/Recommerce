using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Baskets.Dto;
using Recommerce.Services.Customers;
using Recommerce.Services.Products;

namespace Recommerce.Services.Baskets.Implementations;

[UsedImplicitly]
public class BasketService : IBasketService
{
    private readonly AppDbContext _dbContext;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;

    public BasketService(AppDbContext dbContext, ICustomerService customerService, IProductService productService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _productService = productService;
    }

    public async Task<Result<int>> CreateBasketItemAsync(CreateBasketItemInDto basketItemInDto,
        CancellationToken cancellationToken)
    {
        var productIdResult =
            await _productService.GetProductIdAsync(new[] { basketItemInDto.ProductIdentifier }, cancellationToken);
        if (productIdResult.IsFailed)
            return productIdResult.Exception;

        var customerIdResult =
            await _customerService.GetCustomerIdAsync(basketItemInDto.CustomerIdentifier, cancellationToken);
        if (customerIdResult.IsFailed)
            return customerIdResult.Exception;

        var basket = new Basket
        {
            UniqueIdentifier = basketItemInDto.UniqueIdentifier,
            ItemUniqueIdentifier = basketItemInDto.ItemIdentifier,
            CustomerId = customerIdResult.Data,
            ProductId = productIdResult.Data.First().Value,
            Count = basketItemInDto.Count
        };
        await _dbContext.Baskets.AddAsync(basket, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return basket.Id;
    }

    public async Task<Result> DeleteBasketItemAsync(string itemIdentifier, CancellationToken cancellationToken)
    {
        var basketItem = await _dbContext.Baskets
            .Where(b => b.ItemUniqueIdentifier == itemIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (basketItem is null or default(Basket))
            return new EntityNotFoundException<Basket>(itemIdentifier);

        basketItem.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<BasketItemOutDto>>> GetCustomerBasketListAsync(string customerIdentifier,
        CancellationToken cancellationToken)
    {
        var basketItems = await (from basket in _dbContext.Baskets
                join product in _dbContext.Products
                    on basket.ProductId equals product.Id
                join customer in _dbContext.Customers
                    on basket.CustomerId equals customer.Id
                where customer.UniqueIdentifier == customerIdentifier
                select new BasketItemOutDto(
                    basket.UniqueIdentifier,
                    basket.ItemUniqueIdentifier,
                    product.UniqueIdentifier,
                    basket.Count
                )
            ).AsNoTracking()
            .ToListAsync(cancellationToken);

        return basketItems;
    }
}