using $safeprojectname$.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DependencyRegistration
{
    internal static class EntityFramework
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IDbContext, DatabaseContext>();
            return services;
        }
    }
}
