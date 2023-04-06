using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recommerce.Data.Entities;

public class User : IdentityUser<int>, IEntityMarker
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.Property(x => x.Firstname)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.Lastname)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("bit");

        entity.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit");
    }
}