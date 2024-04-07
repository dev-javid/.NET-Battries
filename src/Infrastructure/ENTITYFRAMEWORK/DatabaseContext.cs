namespace $safeprojectname$.EntityFramework;

internal class DatabaseContext(DbContextOptions<DatabaseContext> options) :
    IdentityDbContext<User, IdentityRole<int>, int>(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine);
    }
}
