namespace Recommerce.Identity.Constants;

internal class TokenValidationMessageConstants
{
    public const string InvalidRefreshTokenErrorMessage = "رفرش توکن شما معتبر نمی باشد .";
    public const string RefreshTokenExpirationErrorMessage = "رفرش توکن شما منقضی شده است .";
    public const string RefreshTokenAlreadyUsedErrorMessage = "این رفرش توکن قبلا استفاده شده است .";
    public const string RefreshTokenNotFoundErrorMessage = "این رفرش توکن قبلا استفاده شده است .";
    public const string JwtHasNotExpiredErrorMessage = "توکن شما منقضی نشده و قابل استفاده می باشد .";
    public const string InvalidJwtErrorMessage = "توکن شما معتبر نمی باشد .";
}