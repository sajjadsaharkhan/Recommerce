using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class Order : IEntityMarker
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int Count { get; set; }
    public int UniquePrice { get; set; }
    public int ProductId { get; set; }
    public int? CustomerLocationId { get; set; }
    public int? CustomerSessionId { get; set; }

    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual Customer Customer { get; set; }
    public virtual Product Product { get; set; }
    public virtual CustomerLocation CustomerLocation { get; set; }
    public virtual CustomerSession CustomerSession { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");
        
        entity.Property(x => x.Count)
            .IsRequired()
            .HasColumnType("int");
        
        entity.Property(x => x.UniquePrice)
            .IsRequired()
            .HasColumnType("int");
        
        entity.Property(x => x.UniquePrice)
            .IsRequired()
            .HasColumnType("int");
        
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);
        
        entity.HasOne(x => x.CustomerLocation)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerLocationId);
        
        entity.HasOne(x => x.CustomerSession)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerSessionId);
        
        entity.HasOne(x => x.Product)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ProductId);
    }
}