# Multiple DbContexts with multiple DbSets: how to resolve DbSet&lt;T&gt;?
With the advent of version 6 of Entity Framework, a great new feature came along: multiple DbContexts. One database can now persist classes 
separated to any number of DbContexts. This greatly enhances seperation of concerns and suits especially well for larger systems. Even more, if one uses 
domain driven design, then a DbContext can easily map to a single subdomain. Instead of one big facade-like DbContext, more subdomains map to multiple, targeted DbContexts.

There is however, a challenge here. For a given DbContext there is a number of classes it persists. A second DbContext persists another set of classes. How does one know for a given class, which DbContexts persists it's instances? With multiple DbContexts, each exposing a number of DbSets, this doesn't seem straightforward.

Luckily, there is a way to tell for a specific DbContext, which domain classes it persists. The DbContext class exposes the DbSet for each of the classes it persists.

	public Subdomain1DbContext: DbContext
	{
		public DbSet<DomainObject1> DomainObjects1 { get; set; }
		public DbSet<DomainObject2> DomainObjects2 { get; set; }
		:
		:
	}

Since a DbSet is always of a type T, it is possible to tell if a given DbContext class has a public property of DbSet<T>. Now with reflection, it's possible to determine if the class is persisted via the given DbContext.

	var type = dbContext.GetType();
	var properties = type.GetProperties();
	foreach (var property in properties)
	{
		if (property.PropertyType == typeof (DbSet<T>))
		{
			return true;
		}
	}

Finally, to determine the proper DbContext for a given type of T, all that's required is the list of DbContext classes and the type T, for which to resolve the DbSet.

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

With dependency injection, the list of existing DbContexts can be injected automatically. A full-code example with dependency injection, and generics as well is available at github. Happy coding!
