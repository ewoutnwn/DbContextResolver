namespace Template
{
    using Template.Data;
	
    public interface IDbContextResolver
    {
        IDbContext Resolve<T>() where T : class;
    }
}
