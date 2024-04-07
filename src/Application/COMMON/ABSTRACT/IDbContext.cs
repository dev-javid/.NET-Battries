using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace $safeprojectname$.Common.Abstract
{
    public interface IDbContext : IDisposable
    {
        public DbSet<User> Users { get; set; }

        public DatabaseFacade Database { get; }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        public DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
