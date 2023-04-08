using Microsoft.EntityFrameworkCore;
using Recommerce.Data.Entities;

namespace Recommerce.Data.Extensions;

public static class QueryFilterExtensions
{
    public static void AddQueryFilters(this ModelBuilder builder)
    {
        builder.Entity<CustomerLocation>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Customer>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<CustomerSession>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<CustomerWishList>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ProductReviewMapping>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ProductCategoryMapping>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<CustomerSessionProductMapping>().HasQueryFilter(x => !x.IsDeleted);
    }
}