namespace Recommerce.Data;

public interface IEntityMarker
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
}