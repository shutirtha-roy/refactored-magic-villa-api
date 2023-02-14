using Autofac;
using MagicVilla_VillaAPI.Model;

namespace MagicVilla_VillaAPI
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VillaCreateModel>()
                .AsSelf();

            builder.RegisterType<VillaListModel>()
                .AsSelf();

            builder.RegisterType<VillaEditModel>()
                .AsSelf();

            builder.RegisterType<VillaNumberCreateModel>()
                .AsSelf();

            builder.RegisterType<VillaNumberListModel>()
                .AsSelf();

            builder.RegisterType<VillaNumberEditModel>()
                .AsSelf();

            builder.RegisterType<APIResponse>()
                .AsSelf();

            base.Load(builder);
        }
    }
}
