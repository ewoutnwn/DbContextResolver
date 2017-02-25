namespace Template.Data.Subdomain2
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using Domain.Configuration;

    public class DbContext2 : DbContext, IProjectsDbContext
    {
        public DbSet<DomainObject3> DomainObjects3 { get; set; }
        public DbSet<DomainObject4> DomainObjects4 { get; set; }

        public DbContext1() : base("DbConnectionName")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("subdomain1");
            :
            :
            base.OnModelCreating(modelBuilder);
        }

    }
}
