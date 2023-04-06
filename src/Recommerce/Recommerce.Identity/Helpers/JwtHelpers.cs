using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project.Identity.Dto.Jwt;
using Project.Identity.Extensions;
using JwtConstants = Recommerce.Identity.Constants.JwtConstants;

namespace Recommerce.Identity.Helpers;

internal static class JwtHelpers
{
    private const string SecurityStampClaimType = "SecurityStamp";

    public static JwtDto GenerateToken(JwtCredentialDto credentialDto)
    {
        var tokenDescriptor = _getSecurityTokenDescriptor(credentialDto);
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(securityToken);
        var jti = tokenDescriptor.Subject.FindFirstValue(JwtRegisteredClaimNames.Jti);
        return new JwtDto(jwt, jti);
    }


    public static string GenerateTokenForRefreshToken()
    {
        return Guid.NewGuid().ToString().Replace("-", null);
    }

    public static ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParams = GetTokenValidationParameters();
        tokenValidationParams.ValidateLifetime = false;

        var principal =
            tokenHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);

        return !_isJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
    }

    public static bool TokenIsExpired(ClaimsPrincipal claimsPrincipal)
    {
        var isValid =
            long.TryParse(claimsPrincipal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp).Value,
                out var tokenExpireDateUnix);
        if (!isValid)
            throw new InvalidCastException();

        var tokenExpireDateUtc = DateTime.MinValue.ToUniversalTime()
            .AddSeconds(tokenExpireDateUnix);

        return tokenExpireDateUtc <= DateTime.UtcNow;
    }

    internal static TokenValidationParameters GetTokenValidationParameters()
    {
        var secretKey = Encoding.ASCII.GetBytes(JwtConstants.JwtKey);
        return new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            RequireSignedTokens = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ValidateAudience = true,
            ValidAudience = JwtConstants.JwtAudience,

            ValidateIssuer = true,
            ValidIssuer = JwtConstants.JwtIssuer
        };
    }

    private static bool _isJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken)
               && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                   StringComparison.InvariantCultureIgnoreCase);
    }

    private static SecurityTokenDescriptor _getSecurityTokenDescriptor(JwtCredentialDto credentialDto)
    {
        return new SecurityTokenDescriptor
        {
            SigningCredentials = _getSigningCredentials(),
            Subject = new ClaimsIdentity(_getUserClaims(credentialDto)),
            Issuer = JwtConstants.JwtIssuer,
            Audience = JwtConstants.JwtAudience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(JwtConstants.JwtNotBeforeTimeMinutes),
            Expires = DateTime.Now.AddMinutes(JwtConstants.JwtExpireTimeMinutes)
        };
    }


    private static SigningCredentials _getSigningCredentials()
    {
        var secretKey = Encoding.UTF8.GetBytes(JwtConstants.JwtKey);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512);
        return signingCredentials;
    }

    private static IEnumerable<Claim> _getUserClaims(JwtCredentialDto credentialDto)
    {
        var (userId, userName, securityStamp) = credentialDto;
        var claimsList = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userName),
            new(SecurityStampClaimType, securityStamp),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return claimsList;
    }
}