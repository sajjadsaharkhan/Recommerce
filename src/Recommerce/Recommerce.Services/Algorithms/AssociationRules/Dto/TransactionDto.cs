namespace Recommerce.Services.Algorithms.AssociationRules.Dto;

public class TransactionDto
{
    public List<string> Items { get; }

    public TransactionDto(IEnumerable<string> items)
    {
        Items = items.ToList();
    }
    
    public bool Contains(ItemSetDto itemSet)
    {
        return itemSet.Items.All(item => Items.Contains(item));
    }
}