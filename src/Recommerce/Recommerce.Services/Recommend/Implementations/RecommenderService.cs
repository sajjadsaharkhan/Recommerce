using FluentAssociation;
using JetBrains.Annotations;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Customers;
using Recommerce.Services.Orders;
using Recommerce.Services.Recommend.Dto;

namespace Recommerce.Services.Recommend.Implementations;

[UsedImplicitly]
public class RecommenderService : IRecommenderService
{
    // minimum number of transactions an ItemSetDto must appear in to be considered frequent
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;

    public RecommenderService(IOrderService orderService, ICustomerService customerService)
    {
        _orderService = orderService;
        _customerService = customerService;
    }

    public async Task<Result<IList<string>>> GetRecommendationAsync(
        RecommendationInDto recommendationInDto, CancellationToken cancellationToken)
    {
        var customerIdResult =
            await _customerService.GetCustomerIdAsync(recommendationInDto.CustomerIdentifier, cancellationToken);
        var ordersData = await _orderService.GetOrderedProductIdentifierAsync(customerIdResult.Data,
            recommendationInDto.PreventRepetitiveProducts, cancellationToken);

        var transactions = ordersData.Data
            .Select(order => order.Value)
            .ToList();

        var fluentAssociation = new FluentAssociation<string>();
        fluentAssociation.LoadDataWarehouse(transactions);

        var minSupport = recommendationInDto.AccuracyPercentage / 100f;

        var metrics = await fluentAssociation.GetReportItemSets();
        var bestSupports = metrics
            .Where(x => x.Suport > minSupport)
            .ToList();

        return bestSupports
            .SelectMany(x => x.Items)
            .Take(recommendationInDto.ProductCount)
            .ToList();
    }
}