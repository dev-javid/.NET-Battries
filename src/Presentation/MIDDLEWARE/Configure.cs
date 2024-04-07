using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using $safeprojectname$.Filters;
using Serilog;
using Serilog.Ui.Web;

namespace $safeprojectname$.Middleware
{
    public static class Configure
    {
        public static void ConfigureMiddleware(this WebApplication app, string corsPolicy)
        {
            app.UseHttpsRedirection();
            app.UseCors(corsPolicy);
            app.UseHealthChecks("/health");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilogUi(options =>
            {
                options.Authorization.AuthenticationType = AuthenticationType.Jwt;
                options.Authorization.Filters = [new LogAuthorizeFilter()];
            });

            app.MapControllers();
        }
    }
}
