using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Debugging;

namespace $safeprojectname$.DependencyRegistration
{
    internal static class SerilogSink
    {
        public static void AddPostgresSerilogSink(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            SelfLog.Enable(Console.Error);
        }
    }
}
