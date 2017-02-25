namespace Template.Data.Subdomain2
{
    using System.Data.Entity;
    using Domain.Subdomain2;

    public interface ISubdomain2DbContext : IDbContext
    {
        DbSet<DomainObject3> DomainObjects3 { get; set; }
        DbSet<DomainObject4> DomainObjects4 { get; set; }
    }
}
