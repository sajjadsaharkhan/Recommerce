using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class CustomerSessionProductMapping : IEntityMarker
{
    public int Id { get; set; }
    public int CustomerSessionId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual CustomerSession CustomerSession { get; set; }
    public virtual Product Product { get; set; }
}

public class CustomerSessionProductMappingConfiguration : IEntityTypeConfiguration<CustomerSessionProductMapping>
{
    public void Configure(EntityTypeBuilder<CustomerSessionProductMapping> entity)
    {
        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");

        entity.HasOne(x => x.Product)
            .WithMany(x => x.CustomerSessionProductMappings)
            .HasForeignKey(x => x.ProductId);
        
        entity.HasOne(x => x.CustomerSession)
            .WithMany(x => x.CustomerSessionProductMappings)
            .HasForeignKey(x => x.CustomerSessionId);
    }
}