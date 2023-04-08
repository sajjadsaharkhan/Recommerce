using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class Product : IEntityMarker
{
    public int Id { get; set; }
    public Guid UniqueIdentifier { get; set; }
    public string Name { get; set; }
    public int? BrandId { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public int? WeightInKg { get; set; }
    public float? ReviewRate { get; set; }
    public int? CommentCount { get; set; }
    public int Price { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<CustomerSessionProductMapping> CustomerSessionProductMappings { get; set; }
    public virtual ICollection<CustomerWishList> CustomerWishLists { get; set; }
    public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; }
    public virtual ICollection<ProductReviewMapping> ProductReviewMappings { get; set; }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.Property(x => x.UniqueIdentifier)
            .IsRequired()
            .HasColumnType("varChar(50)");

        entity.Property(x => x.BrandId)
            .HasColumnType("int");

        entity.Property(x => x.WeightInKg)
            .HasColumnType("int");

        entity.Property(x => x.ReviewRate)
            .HasColumnType("float");

        entity.Property(x => x.CommentCount)
            .HasColumnType("int");
        
        entity.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("int");

        entity.Property(x => x.Size)
            .HasColumnType("nvarchar(500)");

        entity.Property(x => x.Color)
            .HasColumnType("nvarchar(500)");

        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");
    }
}