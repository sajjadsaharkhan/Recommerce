using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class Basket : IEntityMarker
{
    public int Id { get; set; }
    public string UniqueIdentifier { get; set; }
    public string ItemUniqueIdentifier { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Product Product { get; set; }
}

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> entity)
    {
        entity.Property(x => x.UniqueIdentifier)
            .IsRequired()
            .HasColumnType("varChar(50)");
        
        entity.Property(x => x.ItemUniqueIdentifier)
            .IsRequired()
            .HasColumnType("varChar(50)");
        
        entity.Property(x => x.Count)
            .HasColumnType("int");

        entity.HasOne(x => x.Product)
            .WithMany(x => x.Baskets)
            .HasForeignKey(x => x.ProductId);

        entity.HasOne(x => x.Customer)
            .WithMany(x => x.Baskets)
            .HasForeignKey(x => x.CustomerId);

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");

        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");
    }
}