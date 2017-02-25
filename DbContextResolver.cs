namespace Template
{
    using System.Collections.Generic;
    using System.Data.Entity;
	using Template.Data;

    public class DbContextResolver : IDbContextResolver
    {
        private readonly IEnumerable<IDbContext> _dbContexts;

        public DbContextResolver(IEnumerable<IDbContext> dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public IDbContext Resolve<T>() where T: class
        {
            foreach (var dbContext in _dbContexts)
            {
                var type = dbContext.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof (DbSet<T>))
                    {
                        return dbContext;
                    }
                }
            }

            return null;
        }
    }
}
