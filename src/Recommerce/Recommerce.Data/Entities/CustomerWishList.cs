using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class CustomerWishList : IEntityMarker
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreationDate { get; set; }
    
    public virtual Customer Customer { get; set; }
    public virtual Product Product { get; set; }
}

public class CustomerWishListConfiguration : IEntityTypeConfiguration<CustomerWishList>
{
    public void Configure(EntityTypeBuilder<CustomerWishList> entity)
    {
        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");
        
        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.HasOne(x => x.Product)
            .WithMany(x => x.CustomerWishLists)
            .HasForeignKey(x => x.ProductId);
        
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerWishLists)
            .HasForeignKey(x => x.CustomerId);
    }
}