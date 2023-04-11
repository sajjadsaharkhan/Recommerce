using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Data.Enums;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DiagController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public DiagController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> AddFakeProducts(CancellationToken cancellationToken)
    {
        var sizeArray = new[] { "S", "M", "L", "XL", "XXL" };

        var productFaker = new Faker<Product>()
            .RuleFor(o => o.UniqueIdentifier, _ => Guid.NewGuid().ToString())
            .RuleFor(o => o.BrandId, f => f.Random.Number(1, 20))
            .RuleFor(o => o.Name, f => f.Commerce.ProductName())
            .RuleFor(o => o.Color, f => f.Commerce.Color())
            .RuleFor(o => o.Size, f => f.PickRandom(sizeArray))
            .RuleFor(o => o.WeightInKg, f => f.Random.Number(1, 10))
            .RuleFor(o => o.ReviewRate, f => f.Random.Float(1, 5))
            .RuleFor(o => o.CommentCount, f => f.Random.Number(1, 1000))
            .RuleFor(o => o.Price, f => f.Random.Number(100, 100000))
            .RuleFor(o => o.CreationDate, f => f.Date.Past(2));

        await _dbContext.Products.AddRangeAsync(productFaker.Generate(100), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> AddFakeCustomers(CancellationToken cancellationToken)
    {
        var customerFaker = new Faker<Customer>()
            .RuleFor(o => o.UniqueIdentifier, _ => Guid.NewGuid().ToString())
            .RuleFor(o => o.BirthDate, f => f.Date.Past(80).OrNull(f, .4f))
            .RuleFor(o => o.GenderType, f => f.PickRandom<GenderType>())
            .RuleFor(o => o.ShoppingBalance, f => f.Random.Number(100, 100000))
            .RuleFor(o => o.LastLoginDate, f => f.Date.Past().OrNull(f, .4f))
            .RuleFor(o => o.CreationDate, f => f.Date.Past(2));

        await _dbContext.Customers.AddRangeAsync(customerFaker.Generate(100), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> AddFakeOrders(CancellationToken cancellationToken)
    {
        var productIdList = await _dbContext.Products
            .AsNoTracking()
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var customerIdList = await _dbContext.Customers
            .AsNoTracking()
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var guidOrders = Enumerable.Range(1, 100)
            .Select(_ => new
            {
                Guid = Guid.NewGuid().ToString(),
                CustomerId = customerIdList[new Random().Next(productIdList.Count)]
            }).ToList();

        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.OrderUniqueIdentifier, f => f.PickRandom(guidOrders).Guid)
            .RuleFor(o => o.CustomerId, f => f.PickRandom(guidOrders).CustomerId)
            .RuleFor(o => o.ProductId, f => f.PickRandom(productIdList))
            .RuleFor(o => o.Count, f => f.Random.Number(1, 25))
            .RuleFor(o => o.UniquePrice, f => f.Random.Number(100, 100000))
            .RuleFor(o => o.CreationDate, f => f.Date.Past(2));

        await _dbContext.Orders.AddRangeAsync(orderFaker.Generate(300), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> AddFakeBaskets(CancellationToken cancellationToken)
    {
        var productIdList = await _dbContext.Products
            .AsNoTracking()
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var customerIdList = await _dbContext.Customers
            .AsNoTracking()
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var guidBaskets = Enumerable.Range(1, 100)
            .Select(_ => new
            {
                Guid = Guid.NewGuid().ToString(),
                CustomerId = customerIdList[new Random().Next(productIdList.Count)]
            }).ToList();

        var basketFaker = new Faker<Basket>()
            .RuleFor(o => o.UniqueIdentifier, f => f.PickRandom(guidBaskets).Guid)
            .RuleFor(o => o.ItemUniqueIdentifier, _ => Guid.NewGuid().ToString())
            .RuleFor(o => o.CustomerId, f => f.PickRandom(guidBaskets).CustomerId)
            .RuleFor(o => o.ProductId, f => f.PickRandom(productIdList))
            .RuleFor(o => o.Count, f => f.Random.Number(1, 25))
            .RuleFor(o => o.CreationDate, f => f.Date.Past(2));

        await _dbContext.Baskets.AddRangeAsync(basketFaker.Generate(300), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}