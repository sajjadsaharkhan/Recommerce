using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Recommerce.Infrastructure.Filters;

namespace Recommerce.Infrastructure.Extensions;

/// <summary>
/// Includes 'ServiceCollection' extensions
/// </summary>
[PublicAPI]
public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAndConfigureSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {
            var allXmlDocumentFiles = AppDomain.CurrentDomain.GetAssemblies()
                .Select(r => Path.Combine(AppContext.BaseDirectory, r.GetName().Name + ".xml"))
                .Where(File.Exists)
                .ToArray();

            foreach (var path in allXmlDocumentFiles)
                setup.IncludeXmlComments(path);

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    internal static IMvcBuilder AddAndConfigureControllers(this IServiceCollection services)
    {
        return services.AddControllers(setup =>
            {
                setup.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

                setup.OutputFormatters.Clear();

                setup.OutputFormatters.Add(new ApiResultOutputFormatter());
            })
            .AddJsonOptions(j =>
            {
                j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                j.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            })
            .ConfigureApiBehaviorOptions(setup =>
            {
                setup.InvalidModelStateResponseFactory = action =>
                {
                    var validationErrors = action.ModelState.ToDictionary(r => r.Key,
                        r => r.Value?.Errors.Select(q => q.ErrorMessage).ToList() ?? new List<string> { "[Unknown]" });
                    var hostEnvironment = action.HttpContext.RequestServices.GetRequiredService<IHostEnvironment>();

                    var result = ApiResult.Failed(validationErrors);
                    if (!hostEnvironment.IsProduction())
                        result.Exception = JsonConvert.SerializeObject(action.ModelState);

                    return new ObjectResult(result) { StatusCode = 400 };
                };
            });
    }
}