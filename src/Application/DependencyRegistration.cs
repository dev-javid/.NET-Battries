using System.Reflection;
using $safeprojectname$.Common.MediatRBehaviour;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR((options) =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TrimmingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);
        services.AddSingleton<IDateTime, DateTimeProvider>();
        return services;
    }
}
