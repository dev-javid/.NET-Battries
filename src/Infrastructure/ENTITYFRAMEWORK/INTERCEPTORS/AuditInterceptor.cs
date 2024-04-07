using Microsoft.EntityFrameworkCore.Diagnostics;

namespace $safeprojectname$.EntityFramework.Interceptors;

public class AuditInterceptor(ICurrentUser currentUser, IDateTime dateTime) : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        UpdateEntities(eventData.Context);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext == null)
        {
            return;
        }

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            var entityType = entry.Metadata;

            var isValueObject = entityType.ClrType.IsSubclassOf(typeof(ValueObject));
            if (isValueObject)
            {
                continue;
            }

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedBy").CurrentValue = currentUser.Id;
                entry.Property("CreatedOn").CurrentValue = dateTime.UtcNow;
                entry.Property("ModifiedBy").CurrentValue = null;
                entry.Property("ModifiedOn").CurrentValue = null;
            }
            else if (entry.State != EntityState.Deleted)
            {
                entry.Property("ModifiedBy").CurrentValue = currentUser.Id;
                entry.Property("ModifiedOn").CurrentValue = dateTime.UtcNow;
            }
        }
    }
}
