namespace StorageManager.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(Type entityType);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}