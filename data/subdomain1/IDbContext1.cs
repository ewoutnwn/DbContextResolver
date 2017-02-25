namespace Template.Data.Subdomain1
{
    using System.Data.Entity;
    using Domain.Subdomain1

    public interface ISubdomain1DbContext : IDbContext
    {
        DbSet<DomainObject1> DomainObjects1 { get; set; }
        DbSet<DomainObject2> DomainObjects2 { get; set; }
    }
}
