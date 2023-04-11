using Recommerce.Services.Algorithms.AssociationRules.Dto;

namespace Recommerce.Services.Algorithms.AssociationRules;

public static class Apriori
{
    public static List<ItemSetDto> GenerateFrequentItemSets(List<TransactionDto> transactions, double minSupport)
    {
        var frequentItemSets = new List<ItemSetDto>();

        var candidateCounts = new Dictionary<ItemSetDto, int>();

        // Count the occurrences of individual items
        var itemCounts = new Dictionary<string, int>();
        foreach (var item in transactions.SelectMany(transaction => transaction.Items))
        {
            itemCounts.TryAdd(item, 0);

            itemCounts[item]++;
        }

        // Generate the 1-ItemSets
        var itemSets = new List<ItemSetDto>();
        foreach (var item in itemCounts.Keys)
        {
            var itemSet = new ItemSetDto(item);
            itemSets.Add(itemSet);
            candidateCounts[itemSet] = itemCounts[item];
        }

        // Generate the frequent ItemSets of size 2 or greater
        while (itemSets.Any())
        {
            var newSets = new List<ItemSetDto>();

            // Generate candidate ItemSets by joining the previous sets
            for (var i = 0; i < itemSets.Count; i++)
            {
                for (var j = i + 1; j < itemSets.Count; j++)
                {
                    var newItemSet = itemSets[i].Join(itemSets[j]);
                    if (newItemSet != null)
                    {
                        newSets.Add(newItemSet);
                    }
                }
            }

            // Count the occurrences of the candidate ItemSets
            candidateCounts.Clear();
            foreach (var transaction in transactions)
            {
                foreach (var candidateSet in newSets)
                {
                    if (!transaction.Contains(candidateSet))
                        continue;
                    
                    candidateCounts.TryAdd(candidateSet, 0);
                    candidateCounts[candidateSet]++;
                }
            }

            // Prune the candidate ItemSets that don't meet the minimum support
            itemSets = newSets.Where(s => candidateCounts.GetValueOrDefault(s) >= minSupport * transactions.Count).ToList();

            // Add the frequent ItemSets to the result list
            frequentItemSets.AddRange(itemSets);
        }

        return frequentItemSets;
    }

    public static IList<AssociationRuleDto> GenerateAssociationRules(List<ItemSetDto> frequentItemSets,
        double minConfidence)
    {
        var rules = new List<AssociationRuleDto>();

        foreach (var itemSet in frequentItemSets)
        {
            if (itemSet.Items.Count <= 1)
                continue;

            var subsets = itemSet.GetSubsets();

            foreach (var antecedent in subsets)
            {
                var consequent = itemSet.Difference(antecedent);

                var confidence = (double)itemSet.Support / antecedent.Support;

                if (!(confidence >= minConfidence))
                    continue;
                var rule = new AssociationRuleDto(antecedent, consequent, confidence);
                rules.Add(rule);
            }
        }

        return rules;
    }
}