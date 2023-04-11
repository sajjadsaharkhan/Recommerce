using Microsoft.EntityFrameworkCore;
using Recommerce.Data.Entities;
using Recommerce.Data.Extensions;


namespace Recommerce.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerLocation> CustomerLocations { get; set; }
    public virtual DbSet<CustomerSession> CustomerSessions { get; set; }
    public virtual DbSet<CustomerWishList> CustomerWishList { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductReviewMapping> ProductReviewMappings { get; set; } 
    public virtual DbSet<CustomerSessionProductMapping> CustomerSessionProductMappings { get; set; } 
    public virtual DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; } 


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntityMarker).Assembly;

        // modelBuilder.RegisterAllEntities<IEntityMarker>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.AddPluralizingTableNameConvention();
        // modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddQueryFilters();
    }
}