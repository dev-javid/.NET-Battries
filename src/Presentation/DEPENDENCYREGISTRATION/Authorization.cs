using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DependencyRegistration
{
    public static class Authorization
    {
        public const string Administartor = "admin";
        public const string Employees = "employee";

        public static void AddMvcWithAuth(this IServiceCollection services)
        {
            var @default = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireAssertion(context =>
                {
                    return context.User.IsInRole(Administartor) ||
                          context.User.IsInRole(Employees);
                })
                .Build();

            services.AddAuthorizationBuilder()
                .AddPolicy(Administartor, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(Administartor);
                }));

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(@default));
            });
        }
    }
}
