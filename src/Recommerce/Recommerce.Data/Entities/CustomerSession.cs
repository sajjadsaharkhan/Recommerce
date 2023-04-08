using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class CustomerSession : IEntityMarker
{
    public int Id { get; set; }
    public Guid UniqueIdentifier { get; set; }
    public int CustomerId { get; set; }
    public string DeviceBrand { get; set; }
    public string DeviceOs { get; set; }
    public string DeviceModel { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<CustomerSessionProductMapping> CustomerSessionProductMappings { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}

public class CustomerSessionConfiguration : IEntityTypeConfiguration<CustomerSession>
{
    public void Configure(EntityTypeBuilder<CustomerSession> entity)
    {
        entity.Property(x => x.UniqueIdentifier)
            .IsRequired()
            .HasDefaultValueSql("NewId()")
            .HasColumnType("uniqueIdentifier");

        entity.Property(x => x.DeviceBrand)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.DeviceModel)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.DeviceOs)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");

        entity.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerSessions)
            .HasForeignKey(x => x.CustomerId);
    }
}