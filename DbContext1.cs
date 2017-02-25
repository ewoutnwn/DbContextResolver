namespace Template.Data
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using Domain.Configuration;

    public class DbContext1 : DbContext, IProjectsDbContext
    {
        public DbSet<DomainObject1> DomainObjects1 { get; set; }
        public DbSet<DomainObject2> DomainObjects2 { get; set; }

        public DbContext1() : base("DbConnectionName")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            :
            :
            base.OnModelCreating(modelBuilder);
        }

    }
}
