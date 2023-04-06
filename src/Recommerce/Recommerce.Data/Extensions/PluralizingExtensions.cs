using Microsoft.EntityFrameworkCore;
using PluralizeService.Core;

namespace Recommerce.Data.Extensions;

public static class PluralizeExtensions
{
    /// <summary>
    /// Pluralizing table name like Post to Posts or Person to People
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            entityType.SetTableName(PluralizationProvider.Pluralize(tableName));
        }
    }
}