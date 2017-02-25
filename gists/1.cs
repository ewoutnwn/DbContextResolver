public Subdomain1DbContext: DbContext
{
	public DbSet<DomainObject1> DomainObjects1 { get; set; }
	public DbSet<DomainObject2> DomainObjects2 { get; set; }
	:
	:
}