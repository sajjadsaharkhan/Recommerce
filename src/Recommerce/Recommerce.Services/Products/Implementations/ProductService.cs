using JetBrains.Annotations;
using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Infrastructure.Pagination;
using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Events;
using Recommerce.Services.Products.Dto;

namespace Recommerce.Services.Products.Implementations;

[UsedImplicitly]
public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductService(AppDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<int>> CreateAsync(CreateProductInDto productInDto, CancellationToken cancellationToken)
    {
        var product = productInDto.Adapt<Product>();

        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task<Result> UpdateAsync(string uniqueIdentifier, UpdateProductInDto productInDto,
        CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Where(p => p.UniqueIdentifier == uniqueIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null or default(Product))
            return new EntityNotFoundException<Product>(uniqueIdentifier);

        product.Name = productInDto.Name;
        product.BrandId = productInDto.BrandId;
        product.Size = productInDto.Size;
        product.Color = productInDto.Color;
        product.Embedding = productInDto.Embedding;
        product.WeightInKg = productInDto.WeightInKg;
        product.ReviewRate = productInDto.ReviewRate;
        product.Price = productInDto.Price;

        await _dbContext.SaveChangesAsync(cancellationToken);

        if (productInDto.ReviewRate.HasValue)
            await _publishEndpoint.Publish(new ProductRated(uniqueIdentifier, productInDto.ReviewRate), cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Where(p => p.UniqueIdentifier == uniqueIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null or default(Product))
            return new EntityNotFoundException<Product>(uniqueIdentifier);

        product.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<ProductOutDto>> GetByIdAsync(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var productOutDto = await _dbContext.Products
            .Where(p => p.UniqueIdentifier == uniqueIdentifier)
            .Select(p => p.Adapt<ProductOutDto>())
            .FirstOrDefaultAsync(cancellationToken);

        return productOutDto is null or default(ProductOutDto)
            ? new EntityNotFoundException<Product>(uniqueIdentifier)
            : productOutDto;
    }

    public async Task<Result<PaginationResponseDto<ProductOutDto>>> GetListAsync(
        PaginationRequestDto paginationRequestDto, CancellationToken cancellationToken)
    {
        var paginatedProductList = await _dbContext.Products
            .Select(p => p.Adapt<ProductOutDto>())
            .ToPaginationAsync(paginationRequestDto, cancellationToken);

        return paginatedProductList;
    }

    public async Task<Result<Dictionary<string, int>>> GetProductIdAsync(IEnumerable<string> productIdentifierList,
        CancellationToken cancellationToken)
    {
        var productsIdDictionary = await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIdentifierList.Contains(p.UniqueIdentifier))
            .ToDictionaryAsync(keySelector => keySelector.UniqueIdentifier, valueSelector => valueSelector.Id,
                cancellationToken);

        if (productsIdDictionary.Count != productIdentifierList.Count())
            return new EntityNotFoundException<Product>();

        return productsIdDictionary;
    }
}