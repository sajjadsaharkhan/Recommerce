using Microsoft.AspNetCore.Http;

namespace Project.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static string GetIpAddress(this HttpContext httpContext)
    {
        const string defaultIpAddress = "127.0.0.1";
        return httpContext?.Connection.RemoteIpAddress?.ToString() ?? defaultIpAddress;
    }
}