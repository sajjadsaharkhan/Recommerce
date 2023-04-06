using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Identity.Helpers;

namespace Recommerce.Identity.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterJwtServices(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtHelpers.GetTokenValidationParameters();
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
    }


    
    public static void RegisterIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User,IdentityRole<int>>(options => { options.User.RequireUniqueEmail = false; })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}