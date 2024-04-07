using System.Reflection;
using DbUp;

namespace $safeprojectname$
{
    public class DatabaseManager
    {
        private DatabaseManager()
        {
        }

        public static bool Execute(string connectionString)
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error running DbUp");
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value: "DbUp success!");
            Console.ResetColor();
            return true;
        }
    }
}
