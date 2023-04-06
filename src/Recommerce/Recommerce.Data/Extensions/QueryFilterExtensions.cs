using Microsoft.EntityFrameworkCore;
using Recommerce.Data.Entities;

namespace Recommerce.Data.Extensions;

public static class QueryFilterExtensions
{
    public static void AddQueryFilters(this ModelBuilder builder)
    {
        builder.Entity<Company>().HasQueryFilter(x => !x.IsDeleted);
    }
}