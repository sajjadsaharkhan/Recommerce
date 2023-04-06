namespace Recommerce.Data.Entities;

public class Company : IEntityMarker
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
}