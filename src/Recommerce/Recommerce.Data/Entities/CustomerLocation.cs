using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class CustomerLocation : IEntityMarker
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int? StateId { get; set; }
    public int? CityId { get; set; }
    public string Address { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}

public class CustomerLocationConfiguration : IEntityTypeConfiguration<CustomerLocation>
{
    public void Configure(EntityTypeBuilder<CustomerLocation> entity)
    {
        entity.Property(x => x.StateId)
            .HasColumnType("int");

        entity.Property(x => x.CityId)
            .HasColumnType("int");

        entity.Property(x => x.Address)
            .HasColumnType("nvarchar(500)");

        entity.Property(x => x.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GetDate()")
            .HasColumnType("DateTime");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");

        entity.HasOne(x => x.Customer)
            .WithMany(x => x.CustomerLocations)
            .HasForeignKey(x => x.CustomerId);
    }
}