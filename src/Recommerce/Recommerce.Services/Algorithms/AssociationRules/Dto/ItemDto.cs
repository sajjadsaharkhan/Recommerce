namespace Recommerce.Services.Algorithms.AssociationRules.Dto;

public class ItemDto
{
    public ItemDto(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}