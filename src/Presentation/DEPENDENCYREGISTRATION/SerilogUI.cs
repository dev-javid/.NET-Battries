using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Ui.PostgreSqlProvider;
using Serilog.Ui.Web;

namespace $safeprojectname$.DependencyRegistration
{
    internal static class SerilogUI
    {
        public static void AddSerilogUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerilog();

            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var logTable = configuration.GetValue<string>("Serilog:WriteTo:0:Args:tableName");
            var connectionString = configuration.GetValue<string>("Serilog:WriteTo:0:Args:connectionString");

            services.AddSerilogUi(options =>
                options.UseNpgSql(PostgreSqlSinkType.SerilogSinksPostgreSQLAlternative, connectionString, logTable));
        }
    }
}
