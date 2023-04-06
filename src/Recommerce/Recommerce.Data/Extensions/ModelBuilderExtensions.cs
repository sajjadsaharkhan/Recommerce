using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sentry;

namespace Recommerce.Data.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Set DeleteBehavior.Restrict by default for relations
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    /// <summary>
    /// Dynamic load all IEntityTypeConfiguration with Reflection
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var applyGenericMethod = typeof(ModelBuilder).GetMethods()
            .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

        foreach (var type in types)
        {
            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsConstructedGenericType ||
                    @interface.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>)) continue;
                var applyConcreteMethod =
                    applyGenericMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                applyConcreteMethod.Invoke(modelBuilder, new[] {Activator.CreateInstance(type)});
            }
        }
    }

    /// <summary>
    /// Dynamic register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(TBaseType).IsAssignableFrom(c));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }


    public static void SeedDatabase(this ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Setting>(builder => builder.HasData(GetInitialSettings()));
    }

    public static void ApplyIdentityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");
        });
        modelBuilder.Entity<IdentityRole<int>>(builder =>
        {
            builder.ToTable("Roles");
            builder.HasData(GetInitialRoles());
        });
        modelBuilder.Entity<IdentityUserRole<int>>(builder =>
        {
            builder.ToTable("UserRoles");
            builder.HasData(GetInitialUserRoles());
        });
        modelBuilder.Entity<IdentityRoleClaim<int>>(builder => { builder.ToTable("RoleClaims"); });
        modelBuilder.Entity<IdentityUserClaim<int>>(builder => { builder.ToTable("UserClaims"); });
        modelBuilder.Entity<IdentityUserLogin<int>>(builder => { builder.ToTable("UserLogins"); });
        modelBuilder.Entity<IdentityUserToken<int>>(builder => { builder.ToTable("UserTokens"); });
    }


    #region private methods
    
    private static IEnumerable<IdentityRole<int>> GetInitialRoles()
    {
        return new List<IdentityRole<int>>
        {
            new()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        };
    }

    private static IEnumerable<IdentityUserRole<int>> GetInitialUserRoles()
    {
        return new List<IdentityUserRole<int>>
        {
            new()
            {
                RoleId = 1,
                UserId = 1
            }
        };
    }

    #endregion
}