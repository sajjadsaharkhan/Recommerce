using System;
using System.Linq;
using System.Reflection;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Recommerce.Infrastructure.Extensions;

/// <summary>
/// Includes 'WebApplicationBuilder' extensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Logging, HttpClient, HttpContextAccessor, Configuration, Caching, Misc, Swagger, AutoRegisterServices, AppMetrics
    /// </summary>
    internal static void RegisterGeneralServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAndConfigureSwaggerServices();
        
        builder.Services.AddAdvancedDependencyInjection();

        builder.Services.AddAndConfigureControllers();
        
        builder.Services.ConfigureCors();

        // config Mapster
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
        
        var moduleAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a=>a.FullName is not null)
            .Where(a => a.FullName.StartsWith("Recommerce"))
            .ToList();
        moduleAssemblies.Add(Assembly.GetEntryAssembly());
        TypeAdapterConfig.GlobalSettings.Scan(moduleAssemblies.ToArray());
    }


    internal static void MapSlash(this IEndpointRouteBuilder builder)
    {
        builder.Map("/", context =>
        {
            var environmentName = context.RequestServices
                .GetRequiredService<IWebHostEnvironment>().EnvironmentName;

            return context.Response.WriteAsync($"Running ({environmentName})");
        });
    }
}