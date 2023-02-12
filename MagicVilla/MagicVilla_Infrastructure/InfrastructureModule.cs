using Autofac;
using MagicVilla_Infrastructure.DbContexts;
using MagicVilla_Infrastructure.Repositories;
using MagicVilla_Infrastructure.Services;
using MagicVilla_Infrastructure.UnitOfWorks;

namespace MagicVilla_Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<VillaService>()
                .As<IVillaService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VillaRepository>()
                .As<IVillaRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VillaNumberService>()
                .As<IVillaNumberService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VillaNumberRepository>()
                .As<IVillaNumberRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>()
                .As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}