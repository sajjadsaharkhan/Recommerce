namespace Recommerce.Identity.Constants;

internal static class JwtConstants
{
    public const string JwtKey = "laksjdfaklsjfoiwejrASWERQBXCVFGaljskfaWQWERYTYHHB";
    public const string JwtAudience = "Bomlet.App";
    public const string JwtIssuer = "Bomlet.App";
    public const int JwtNotBeforeTimeMinutes = 0;
    public const int JwtExpireTimeMinutes = 240;
    public const int RefreshTokenTtlByDay = 5;
}