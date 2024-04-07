using $safeprojectname$;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

IConfiguration configuration = builder.Build();
string connectionString = configuration.GetConnectionString("Default")!;

return DatabaseManager.Execute(connectionString) ? 0 : -1;
