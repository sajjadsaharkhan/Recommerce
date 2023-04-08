using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class ProductCategoryMapping : IEntityMarker
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Product Product { get; set; }
}


public class ProductCategoryMappingConfiguration : IEntityTypeConfiguration<ProductCategoryMapping>
{
    public void Configure(EntityTypeBuilder<ProductCategoryMapping> entity)
    {
        entity.Property(x => x.CategoryId)
            .IsRequired()
            .HasColumnType("int");

        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");

        entity.HasOne(x => x.Product)
            .WithMany(x => x.ProductCategoryMappings)
            .HasForeignKey(x => x.ProductId);
    }
}