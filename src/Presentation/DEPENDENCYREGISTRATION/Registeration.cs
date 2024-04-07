using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DependencyRegistration;

public static class Registeration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration, string corsPolicy)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddHttpContextAccessor();
        services.AddSerilogUi(configuration);
        services.AddHealthChecks();
        services.AddMvcWithAuth();

        services.AddCors(options => options.AddPolicy(corsPolicy,
        policy =>
        {
            var allowedOrigins = configuration.GetSection("AllowedHosts").Get<string[]>() ?? [];
            policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        }));

        return services;
    }
}
