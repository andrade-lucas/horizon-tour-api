using Horizon.Auth.Services;
using Horizon.Auth.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Horizon.Api.Configuration.Extensions;

public static class ServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
    }

    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSecret"));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}
