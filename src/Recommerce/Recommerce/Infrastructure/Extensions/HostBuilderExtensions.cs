using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ILogger = Serilog.ILogger;

namespace Recommerce.Infrastructure.Extensions;

/// <summary>
/// Includes 'IWebHostBuilder' and 'IHostBuilder' extensions
/// </summary>
[PublicAPI]
internal static class HostBuilderExtensions
{
    internal static WebApplication MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var hostEnvironment = services.GetService<IWebHostEnvironment>();
        if (hostEnvironment.IsDevelopment())
            return host as WebApplication;

        var logger = services.GetRequiredService<ILogger>();
        var context = services.GetService<TContext>();


        try
        {
            logger.Information("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            InvokeSeeder(seeder!, context, services);

            logger.Information("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An error occurred while migrating the database used on context {DbContextName}",
                typeof(TContext).Name);
        }

        return host as WebApplication;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services) where TContext : DbContext
    {
        context?.Database.Migrate();
        seeder(context, services);
    }
}