using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommerce.Data.Enums;

namespace Recommerce.Data.Entities;

public class Customer : IEntityMarker
{
    public int Id { get; set; }
    public string UniqueIdentifier { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderType? GenderType { get; set; }
    public int? ShoppingBalance { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual ICollection<CustomerLocation> CustomerLocations { get; set; }
    public virtual ICollection<CustomerSession> CustomerSessions { get; set; }
    public virtual ICollection<CustomerWishList> CustomerWishLists { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<ProductReviewMapping> ProductReviewMappings { get; set; }
}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(x => x.UniqueIdentifier)
            .IsRequired()
            .HasColumnType("varChar(50)");
        
        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");
        
        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.RegisterDate)
            .IsRequired()
            .HasColumnType("DateTime");

        entity.Property(x => x.LastLoginDate)
            .IsRequired()
            .HasColumnType("DateTime");
        
        entity.Property(x => x.BirthDate)
            .HasColumnType("DateTime");

        entity.Property(x => x.ShoppingBalance)
            .IsRequired()
            .HasColumnType("int");
        
        entity.Property(x => x.GenderType)
            .HasColumnType("TinyInt");
    }
}