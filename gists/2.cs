var type = dbContext.GetType();
var properties = type.GetProperties();
foreach (var property in properties)
{
	if (property.PropertyType == typeof (DbSet<T>))
	{
		return true;
	}
}