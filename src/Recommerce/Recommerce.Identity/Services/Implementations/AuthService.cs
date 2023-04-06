using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Project.Identity.Dto;
using Project.Identity.Dto.Jwt;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Identity.Constants;
using Recommerce.Identity.Dto.OtpLogin;
using Recommerce.Identity.Helpers;
using Recommerce.Infrastructure.Exceptions;

namespace Recommerce.Identity.Services.Implementations;

[UsedImplicitly]
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _dbContext;

    public AuthService(UserManager<User> userManager,
        SignInManager<User> signInManage,
        AppDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManage;
        _dbContext = dbContext;
    }

    public async Task ConfirmUserInformationAsync(UserLoginInformationDto userLoginInformationDto)
    {
        var user = await _userManager.FindByIdAsync(userLoginInformationDto.UserId.ToString());
        if (user is null)
            throw new UserFriendlyException(IdentityMessageConstants.UserNotFoundErrorMessage);

        user.Firstname = userLoginInformationDto.FirstName!;
        user.Lastname = userLoginInformationDto.LastName!;

        user.IsActive = true;

        await _updateUserAsync(user);
    }

    public async Task<LoginResponseDataDto> LoginAsAdministratorAsync(string username, string password,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new UserFriendlyException(IdentityMessageConstants.UserNotFoundErrorMessage);
        if (!user.IsActive)
            throw new UserFriendlyException(IdentityMessageConstants.UserIsInActiveErrorMessage);

        var isAdmin = await _userManager.IsInRoleAsync(user, "admin");
        if (!isAdmin)
            throw new NotEnoughAccessException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
            throw new UserFriendlyException(IdentityMessageConstants.UserNotFoundErrorMessage);

        var userJwtCredential = new JwtCredentialDto(user.Id, user.UserName, user.SecurityStamp);
        var (token, refreshToken) = await GenerateNewTokenAsync(userJwtCredential,
            cancellationToken);

        return new LoginResponseDataDto(
            user.Firstname,
            user.Lastname,
            token,
            refreshToken,
            false
        );
    }

    public async Task<TokenResultDto> GenerateNewTokenAsync(JwtCredentialDto credentialDto,
        CancellationToken cancellationToken)
    {
        var (jwtToken, jwtId) = JwtHelpers.GenerateToken(credentialDto);


        await _dbContext.SaveChangesAsync(cancellationToken);

        return new TokenResultDto(jwtToken, null);
    }

    public async Task<LoginResponseDataDto> LoginVendorAsync(string username, string password,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new UserFriendlyException(IdentityMessageConstants.UserNotFoundErrorMessage);
        if (!user.IsActive)
            throw new UserFriendlyException(IdentityMessageConstants.UserIsInActiveErrorMessage);

        var isVendor = await _userManager.IsInRoleAsync(user, "vendor");
        if (!isVendor)
            throw new NotEnoughAccessException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
            throw new UserFriendlyException(IdentityMessageConstants.UserNotFoundErrorMessage);

        var userJwtCredential = new JwtCredentialDto(user.Id, user.UserName, user.SecurityStamp);
        var userJwt = await GenerateNewTokenAsync(userJwtCredential,
            cancellationToken);

        return new LoginResponseDataDto(user.Firstname, user.Lastname, userJwt.Token, userJwt.RefreshToken, false);
    }



    private async Task<User> _createUserAsync(string phoneNumber)
    {
        var user = _initializeUserByPhoneNumber(phoneNumber);
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
            return user;

        var errors = result.Errors.Select(error => $"{error.Code} - {error.Description}");
        throw new Exception(string.Join(",", errors));
    }

    private async Task _updateUserAsync(User user)
    {
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new Exception(updateResult.Errors.ToString());
    }

    private static User _initializeUserByPhoneNumber(string phone)
    {
        return new User
        {
            PhoneNumber = phone,
            UserName = phone,
            NormalizedUserName = phone,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            PhoneNumberConfirmed = false,
            IsActive = false,
            EmailConfirmed = false,
            RegisterDate = DateTime.Now,
            LastLoginDate = DateTime.Now,
            TwoFactorEnabled = true
        };
    }
}