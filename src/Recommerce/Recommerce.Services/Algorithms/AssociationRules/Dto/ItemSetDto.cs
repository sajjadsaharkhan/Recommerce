namespace Recommerce.Services.Algorithms.AssociationRules.Dto;

public class ItemSetDto : Dictionary<List<string>, int>
{
    public HashSet<string> Items { get; }

    public int Support { get; set; }

    public ItemSetDto(params string[] items)
    {
        Items = new HashSet<string>(items);
        Support = 0;
    }

    public ItemSetDto(IEnumerable<string> items)
    {
        Items = new HashSet<string>(items);
        Support = 0;
    }

    public ItemSetDto Join(ItemSetDto other)
    {
        var newItems = new HashSet<string>(Items);
        foreach (var item in other.Items)
        {
            if (!Items.Contains(item))
            {
                newItems.Add(item);
            }
        }

        return newItems.Count > Items.Count 
            ? new ItemSetDto(newItems) 
            : null;
    }

    public ItemSetDto Difference(ItemSetDto other)
    {
        var newItems = new HashSet<string>(Items);
        foreach (var item in other.Items)
        {
            newItems.Remove(item);
        }

        return new ItemSetDto(newItems);
    }

    public List<ItemSetDto> GetSubsets()
    {
        var subsets = new List<ItemSetDto>();

        var newItems = new HashSet<string>();
        for (var i = 0; i < 1 << Items.Count; i++)
        {
            var j = 0;
            foreach (var item in Items)
            {
                if ((i & (1 << j)) != 0)
                {
                    newItems.Add(item);
                }

                j++;
            }

            if (newItems.Count > 0 && newItems.Count < Items.Count)
            {
                subsets.Add(new ItemSetDto(newItems));
            }
        }

        return subsets;
    }

    public override bool Equals(object obj)
    {
        return obj is ItemSetDto other && Items.SetEquals(other.Items);
    }

    public override int GetHashCode()
    {
        return Items.GetHashCode();
    }
}