using $safeprojectname$.Services;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DependencyRegistration;

public static class Registeration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEntityFramework(configuration)
            .AddIdentity(configuration)
            .AddPostgresSerilogSink(configuration);

        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}
