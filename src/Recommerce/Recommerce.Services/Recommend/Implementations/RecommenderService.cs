using JetBrains.Annotations;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Algorithms.AssociationRules;
using Recommerce.Services.Algorithms.AssociationRules.Dto;
using Recommerce.Services.Customers;
using Recommerce.Services.Orders;
using Recommerce.Services.Recommend.Dto;

namespace Recommerce.Services.Recommend.Implementations;

[UsedImplicitly]
public class RecommenderService : IRecommenderService
{
    // minimum number of transactions an ItemSetDto must appear in to be considered frequent
    private const int MinFrequentMeaning = 2;
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
            .Select(order => new TransactionDto(order.Value))
            .ToList();

        // Run Apriori algorithm to find frequent ItemSetDto and association rules
        var frequentItemSets = Apriori.GenerateFrequentItemSets(transactions, MinFrequentMeaning);
        var associationRules =
            Apriori.GenerateAssociationRules(frequentItemSets, recommendationInDto.AccuracyPercentage);

        // Find recommended products based on association rules
        var recommendedProductIdList = associationRules
            .OrderByDescending(ar => ar.Confidence)
            .Select(ar => ar.Consequent.Items.FirstOrDefault())
            .Take(recommendationInDto.ProductCount)
            .ToList();

        return recommendedProductIdList;
    }
}