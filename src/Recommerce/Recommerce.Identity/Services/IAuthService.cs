using Project.Identity.Dto;
using Project.Identity.Dto.Jwt;
using Recommerce.Identity.Dto.OtpLogin;
using Scrutor.AspNetCore;

namespace Recommerce.Identity.Services;

public interface IAuthService : IScopedLifetime
{
    /// <summary>
    /// confirms user 
    /// </summary>
    /// <param name="userLoginInformationDto"></param>
    /// <returns></returns>
    public Task ConfirmUserInformationAsync(UserLoginInformationDto userLoginInformationDto);

    /// <summary>
    /// admin login method
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<LoginResponseDataDto> LoginAsAdministratorAsync(string username, string password,
        CancellationToken cancellationToken);

    /// <summary>
    /// generate new token 
    /// </summary>
    /// <param name="credentialDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TokenResultDto> GenerateNewTokenAsync(JwtCredentialDto credentialDto,
        CancellationToken cancellationToken);

    /// <summary>
    /// login vendor user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<LoginResponseDataDto> LoginVendorAsync(string username, string password,
        CancellationToken cancellationToken);

}