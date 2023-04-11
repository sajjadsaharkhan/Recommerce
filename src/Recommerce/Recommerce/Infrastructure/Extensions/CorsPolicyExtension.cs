using Microsoft.Extensions.DependencyInjection;

namespace Recommerce.Infrastructure.Extensions;

public static class CorsPolicyExtension
{
    public const string MyAllowSpecificOrigins = "BomletClientOrigins";

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://localhost:7065/").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
        });
    }
}