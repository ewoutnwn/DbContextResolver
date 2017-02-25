public DbContext Resolve<T>(IEnumerable<DbContext> dbContexts) where T: class
{
	foreach (var dbContext in dbContexts)
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