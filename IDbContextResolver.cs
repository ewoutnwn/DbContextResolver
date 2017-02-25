namespace StorageManager.Data.Repositories
{
    public interface IDbContextResolver
    {
        IDbContext Resolve<T>() where T : class;
    }
}