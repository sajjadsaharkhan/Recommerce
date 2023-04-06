using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Project.Identity.Extensions;

public static class IdentityExtensions
{
    public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
    {
        var userId = identity.GetUserId();
        return string.IsNullOrEmpty(userId)
            ? default
            : (T)Convert.ChangeType(userId, typeof(T), CultureInfo.InvariantCulture);
    }

    public static int GetUserIdRequired(this IIdentity identity)
    {
        if (identity is null)
            throw new UnauthorizedAccessException();
        
        var strUserId = identity.GetUserId();
        return int.TryParse(strUserId, out var userId)
            ? userId
            : throw new UnauthorizedAccessException();
    }


    public static string GetUserId(this IIdentity identity)
    {
        return identity.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string GetEmail(this IIdentity identity)
    {
        return identity.FindFirstValue(ClaimTypes.Email);
    }


    public static string GetUsername(this IIdentity identity)
    {
        return identity.FindFirstValue(ClaimTypes.Name);
    }

    public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        return identity.FindFirst(claimType)?.Value ?? string.Empty;
    }

    public static string FindFirstValue(this IIdentity identity, string claimType)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        return claimsIdentity?.FindFirstValue(claimType) ?? string.Empty;
    }
}