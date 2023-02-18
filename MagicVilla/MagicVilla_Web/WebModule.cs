using Autofac;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IService;

namespace MagicVilla_Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VillaService>().As<IVillaService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<VillaNumberService>().As<IVillaNumberService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}