namespace Template
{
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;    
    using Template.Data;
    using Template.Data.Subdomain1;
    using Template.Data.Subdomain2;

    public static class Container
    {
        private static volatile IContainer _container;
        private static object syncRoot = new object();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterApiControllers(assembly);

            // Data Access
            builder.RegisterType<DbContext1>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DbContext2>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DbContextResolver>().AsImplementedInterfaces();
            builder.RegisterGeneric(typeof (GenericRepository<>)).As(typeof (IGenericRepository<>));            

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        public static IContainer Instance
        {
            get
            {
                if (_container != null) return _container;
                lock (syncRoot)
                {
                    if (_container == null)
                    {
                        _container = Configure();
                    }
                }

                return _container;
            }
        }

        public static void Dispose()
        {
            if (_container == null) return;
            lock (syncRoot)
            {
                if (_container == null) return;
                _container.Dispose();
                _container = null;
            }
        }
    }
}
