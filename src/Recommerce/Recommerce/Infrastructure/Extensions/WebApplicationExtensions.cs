using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Recommerce.Infrastructure.Extensions;

/// <summary>
/// Includes 'WebApplication' extensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Use Swagger, Slash, AdvancedHealthCheck, MicroserviceExceptionAndStatusCodeHandler
    /// </summary>
    public static void UseGeneralServices(this WebApplication app)
    {
        app.UseHttpsRedirection();
        
        app.UseCors(CorsPolicyExtension.MyAllowSpecificOrigins);

        app.UseRequestTimeout();
        app.UseForwardedHeaders();

        app.UseSwagger();
        app.UseSwaggerUI(setup =>
        {
            setup.DocExpansion(DocExpansion.None);
            setup.DisplayRequestDuration();
        });

        app.UseExceptionAndStatusCodeHandler();
        app.MapSlash();
        if(!app.Environment.IsDevelopment())
            app.UseSentryTracing();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

    }

    private static void UseForwardedHeaders(this IApplicationBuilder app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All };
        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();
        app.UseForwardedHeaders(forwardedHeadersOptions);
    }

    private static void UseRequestTimeout(this IApplicationBuilder app, int getRequestsTimeoutInSecond = 10,
        int nonGetRequestsTimeoutInSecond = 10, int uploadRequestsTimeoutInSecond = 20)
    {
        if (Debugger.IsAttached)
            return;

        app.Use(async (context, next) =>
        {
            var timeoutInSecond = context.Request.Method.ToUpper() == "GET"
                ? getRequestsTimeoutInSecond
                : nonGetRequestsTimeoutInSecond;

            if (context.Request.Path is { Value: "/api/file/upload-file" })
                timeoutInSecond = uploadRequestsTimeoutInSecond;

            
            var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSecond));
            var newToken = CancellationTokenSource
                .CreateLinkedTokenSource(context.RequestAborted, timeoutCts.Token).Token;

            context.RequestAborted = newToken;
            await next.Invoke();
        });
    }
}