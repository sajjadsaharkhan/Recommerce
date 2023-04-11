using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recommerce.Infrastructure.Constants;
using Recommerce.Infrastructure.Exceptions;
using Serilog.Debugging;

namespace Recommerce.Infrastructure.Extensions;

[PublicAPI]
public static class ApplicationBuilderExtensions
{
    private const int WriteToStreamTimeoutInSeconds = 10;

    public static IApplicationBuilder UseExceptionAndStatusCodeHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(_configureExceptionHandler);
        app.UseStatusCodePages(_configureStatusCodePages);
        
        return app;
    }

    private static void _configureStatusCodePages(IApplicationBuilder app)
    {
        app.Run(context =>
        {
            var result = ApiResult.Failed(context.Response.StatusCode is 401 or 403
                ? ApiResultStatusCodeMessageConstants.UnAuthorizedMessage
                : ApiResultStatusCodeMessageConstants.ServerErrorMessage);

            return context.Response.WriteAsJsonAsync(result, context.RequestAborted);
        });
    }

    private static void _configureExceptionHandler(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature == null)
            {
                SelfLog.WriteLine("contextFeature == null");
                return;
            }

            await _handleExceptionsAsync(context, contextFeature.Error);
        });
    }

    private static async Task _handleExceptionsAsync(HttpContext context, Exception exception)
    {
        ApiResult result;


        switch (exception)
        {
            case UserFriendlyException userFriendlyException:
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                result = userFriendlyException.ErrorCode.HasValue
                    ? ApiResult.Failed(userFriendlyException.ErrorCode.Value, userFriendlyException.Message)
                    : ApiResult.Failed(userFriendlyException.Message);
                break;

            case NotEnoughAccessException or UnauthorizedAccessException:
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                result = ApiResult.Failed(HttpStatusCode.Forbidden, "به این قسمت دسترسی ندارید.");
                break;

            case TaskCanceledException:
            case OperationCanceledException:
                context.Response.StatusCode = (int) HttpStatusCode.RequestTimeout;
                result = ApiResult.Failed(HttpStatusCode.RequestTimeout,
                    "در حال حاضر سرور مشغول می باشد. لطفا مجددا تلاش فرمایید.");
                break;

            case ValidationException validationException:
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                result = ApiResult.Failed(validationException.GetErrors());
                break;

            case AntiforgeryValidationException:
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                result = ApiResult.Failed(HttpStatusCode.Processing, "لطفا دوباره تلاش کنید");
                break;

            default:
                var isInstanceOfEntityNotFoundException =
                    exception is NotFoundException ||
                    exception.GetType().IsGenericType &&
                    exception.GetType().GetGenericTypeDefinition() == typeof(EntityNotFoundException<>);

                context.Response.StatusCode = isInstanceOfEntityNotFoundException
                    ? (int) HttpStatusCode.BadRequest
                    : (int) HttpStatusCode.InternalServerError;

                result = isInstanceOfEntityNotFoundException
                    ? ApiResult.Failed(HttpStatusCode.NotFound, "اطلاعات مورد نظر در پایگاه داده وجود ندارد")
                    : ApiResult.Failed(HttpStatusCode.InternalServerError, "خطایی در مرکز رخ داده است.");
                break;
        }

        var hostEnvironment = context.RequestServices.GetRequiredService<IHostEnvironment>();
        if (!hostEnvironment.IsProduction())
            result.Exception = exception.ToString();

        await context.Response.WriteAsJsonAsync(result, context.RequestAborted);
    }
}