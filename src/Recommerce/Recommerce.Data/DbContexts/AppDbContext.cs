using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.Entities;
using Recommerce.Data.Extensions;


namespace Recommerce.Data.DbContexts;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntityMarker).Assembly;

        modelBuilder.RegisterAllEntities<IEntityMarker>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.AddPluralizingTableNameConvention();
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddQueryFilters();
        modelBuilder.ApplyIdentityConfiguration();
    }
}