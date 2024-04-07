using System.Text;
using $safeprojectname$.EntityFramework;
using $safeprojectname$.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace $safeprojectname$.DependencyRegistration;

public static class IdentityRegistration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(IdentityService.TokenProviderName)
            .AddDefaultTokenProviders();

        JwtConfiguration jwtConfiguration = new();
        services.AddSingleton(jwtConfiguration);

        configuration.GetSection("JwtConfiguration").Bind(jwtConfiguration);
        var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            };
        });

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
