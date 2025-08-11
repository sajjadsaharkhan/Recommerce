using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Services.Products.Implementations;
using Xunit;

namespace Recommerce.Tests;

public class ProductServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("ProductServiceTests")
            .Options;

        _dbContext = new AppDbContext(options);

        var publishEndpoint = new Mock<IPublishEndpoint>().Object;
        _productService = new ProductService(_dbContext, publishEndpoint);
    }

    [Fact]
    public async Task GetProductIdAsync_WhenIdentifierMissing_ReturnsEntityNotFound()
    {
        // Arrange
        var product = new Product
        {
            UniqueIdentifier = "existing",
            Name = "Existing Product",
            Price = 1
        };
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        // Act
        var resultMissing = await _productService.GetProductIdAsync(
            new[] { product.UniqueIdentifier, "missing" }, CancellationToken.None);

        // Assert failure
        Assert.True(resultMissing.IsFailed);
        Assert.IsType<EntityNotFoundException<Product>>(resultMissing.Exception);

        // Act success
        var resultSuccess = await _productService.GetProductIdAsync(
            new[] { product.UniqueIdentifier }, CancellationToken.None);

        // Assert success
        Assert.True(resultSuccess.IsSuccess);
        Assert.Equal(product.Id, resultSuccess.Data[product.UniqueIdentifier]);
    }
}
