using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.Entities;

namespace Recommerce.Identity.Extensions;

public static class UserManagerExtensions
{
    /// <summary>
    /// get user by phoneNumber
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public static async Task<User> FindByPhoneAsync(this UserManager<User> userManager, string phoneNumber)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        return user;
    }
}