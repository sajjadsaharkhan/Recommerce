using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommerce.Data.Enums;

namespace Recommerce.Data.Entities;

public class ProductReviewMapping : IEntityMarker
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public string Comment { get; set; }
    public float? Rate { get; set; }
    public ReviewEmotionType EmotionType { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual Customer Customer { get; set; }
    public virtual Product Product { get; set; }
}

public class ProductReviewMappingConfiguration : IEntityTypeConfiguration<ProductReviewMapping>
{
    public void Configure(EntityTypeBuilder<ProductReviewMapping> entity)
    {
        entity.Property(x => x.Comment)
            .IsRequired()
            .HasColumnType("varChar(1000)");

        entity.Property(x => x.Rate)
            .HasColumnType("float");

        entity.Property(x => x.EmotionType)
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
            .WithMany(x => x.ProductReviewMappings)
            .HasForeignKey(x => x.ProductId);
        
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.ProductReviewMappings)
            .HasForeignKey(x => x.CustomerId);
    }
}